using System;
using System.Linq;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Security;
using Enfield.ShopManager.Services;
using Enfield.ShopManager.Filters;

namespace Enfield.ShopManager.Controllers
{
    [BuildMenu()]
    [ValidateToken("Employee,Manager,Administrator")]
    public class ShopFloorController : RestrictedControllerBase
    {
        public ActionResult Index()
        {
            var id = 0;
            var vehicle = NavigationServices.GetVehiclesInShop(base.LocationId).FirstOrDefault();
            if (vehicle != null)
            {
                id = vehicle.Id;
            }

            return RedirectToAction("GetInvoice", new { id = id });
        }

        public ActionResult GetInvoice(int id)
        {
            ShopFloorModel model = CreateShopFloorModel(id, null);

            ViewBag.ActiveEmployees = EmployeeServices.GetActiveEmployees();
            ViewBag.SignedInEmployees = InvoiceServices.GetSignedInEmployeeSelectList(base.LocationId);

            if (model.Invoice == null)
            {
                return View("Index_NoData", model);
            }

            if (!string.IsNullOrEmpty(model.Invoice.StockNumber))
                model.Invoice.History = InvoiceServices.GetStockNumberHistory(model.Invoice);

            return View("Index", model);
        }

        public ActionResult FindInvoice(string invoiceId, string stocknumber)
        {
            int id;
            int.TryParse(invoiceId, out id);

            ShopFloorModel model = CreateShopFloorModel(id, stocknumber);

            ViewBag.ActiveEmployees = EmployeeServices.GetActiveEmployees();
            ViewBag.SignedInEmployees = InvoiceServices.GetSignedInEmployeeSelectList(base.LocationId);

            if (model.Invoice == null)
            {
                return View("Index_NoData", model);
            }

            if (!string.IsNullOrEmpty(model.Invoice.StockNumber))
                model.Invoice.History = InvoiceServices.GetStockNumberHistory(model.Invoice);

            return View("Index", model);
        }

        private ShopFloorModel CreateShopFloorModel(int id, string stocknumber)
        {
            ShopFloorModel model = new ShopFloorModel();

            if (id > 0)
            {
                model.Invoice = InvoiceServices.GetInvoice(id);
                if (model.Invoice == null) model.NoDataMessage = string.Format("We could not find an invoice with an id = {0}", id);
            }
            else if (!string.IsNullOrEmpty(stocknumber))
            {
                model.Invoice = InvoiceServices.GetInvoice(stocknumber);
                if (model.Invoice == null) model.NoDataMessage = string.Format("We could not find an invoice with a stock number = {0}", stocknumber);
            }
            else
            {
                model.NoDataMessage = "There are no vehicles logged into the shop at this time";
            }

            model.Services = InvoiceServices.GetServices(base.LocationId, model.Invoice);
            model.VehiclesInShop = NavigationServices.GetVehiclesInShop(base.LocationId).OrderBy(o => o.Id).ToList();
            model.VehiclesCompleted = NavigationServices.GetVehiclesCompletedToday(base.LocationId).OrderBy(o => o.Id).ToList();

            return model;
        }

        public ActionResult NewVehicle()
        {
            var model = new NewInvoiceModel();

            var location = LocationServices.GetLocation(base.LocationId);
            if (location.DefaultAccountId != 0)
            {
                var defaultAccount = AccountServices.GetAccount(location.DefaultAccountId);
                model.AccountName = defaultAccount.Name;
                model.AccountId = defaultAccount.Id;
            }

            ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(false, model.AccountTypeId);

            return View("NewVehicle", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewVehicle(NewInvoiceModel invoice)
        {
            if (ModelState.IsValid)
            {
                var newInvoice = InvoiceServices.CreateInvoice(invoice, base.LocationId);
                NavigationServices.AddVehicleToInShopList(base.LocationId, newInvoice);
                return RedirectToAction("GetInvoice", new { id = newInvoice.Id });
            }

            // Invalid - redisplay with errors
            ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(false, invoice.AccountTypeId);

            return View("NewVehicle", invoice);
        }

        #region | Ajax Actions |

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateAccount(int invoiceId, int accountId)
        {
            InvoiceServices.UpdateAccount(invoiceId, accountId);
            return Json(new { result = "ok" });
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateInvoice(int invoiceId, string field, string value)
        {
            InvoiceServices.UpdateInvoice(invoiceId, field, value);
            return Json(new { result = "ok" });
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompleteVehicle(int invoiceId)
        {
            var invoice = InvoiceServices.CompleteInvoice(invoiceId);
            if (string.IsNullOrEmpty(invoice.StockNumber)) throw new InvalidOperationException("The stock number cannot be empty");

            NavigationServices.RemoveVehicleFromInShopList(base.LocationId, invoice);
            NavigationServices.AddVehicleToCompletedTodayList(base.LocationId, invoice);

            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RecallVehicle(int invoiceId)
        {
            var invoice = InvoiceServices.RecallInvoice(invoiceId);
            NavigationServices.AddVehicleToInShopList(base.LocationId, invoice);
            if (invoice.CompleteDate > DateTime.Today)
            {
                NavigationServices.RemoveVehicleFromCompletedTodayList(base.LocationId, invoice);
            }

            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteInvoice(int invoiceId)
        {
            var invoice = InvoiceServices.DeleteInvoice(invoiceId);
            NavigationServices.ClearCache(base.LocationId);

            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddHistory(int invoiceId, string note)
        {
            var history = InvoiceServices.AddHistory(invoiceId, note);
            return PartialView("_HistoryList", history);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddService(int invoiceId, int serviceTypeId, decimal? rate)
        {
            decimal initialRate = 0;
            if (rate.HasValue) initialRate = rate.Value;
            var invoice = InvoiceServices.AddService(invoiceId, serviceTypeId, initialRate);
            return PartialView("_ServiceList", invoice.ServiceList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditServiceRate(int invoiceId, int serviceId, decimal rate)
        {
            var invoice = InvoiceServices.UpdateServiceRate(invoiceId, serviceId, rate);
            return PartialView("_ServiceList", invoice.ServiceList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteService(int invoiceId, int serviceId)
        {
            var invoice = InvoiceServices.DeleteService(invoiceId, serviceId);
            return PartialView("_ServiceList", invoice.ServiceList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddLabor(int invoiceId, int laborTypeId, decimal rate)
        {
            var invoice = InvoiceServices.AddLabor(invoiceId, laborTypeId, rate);
            return PartialView("_LaborList", invoice.LaborList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEmployeeToLabor(int invoiceId, int laborId, int employeeId)
        {
            var invoice = InvoiceServices.UpdateLabor(invoiceId, laborId, employeeId, base.LocationId);
            return PartialView("_LaborList", invoice.LaborList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLaborRate(int invoiceId, int laborId, decimal rate)
        {
            var invoice = InvoiceServices.UpdateLaborRate(invoiceId, laborId, rate);
            return PartialView("_LaborList", invoice.LaborList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteLabor(int invoiceId, int laborId)
        {
            var invoice = InvoiceServices.DeleteLabor(invoiceId, laborId);
            return PartialView("_LaborList", invoice.LaborList);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(int id)
        {
            var employee = EmployeeServices.GetUser(id);
            if (employee == null) throw new ArgumentException(string.Format("No employee found with an id = {0}", id));

            SecurityServices.SignIn(employee, base.LocationId);
            var employees = InvoiceServices.GetSignedInEmployees(base.LocationId, forceRefresh: true);

            return PartialView("_AvailableEmployees", employees);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignOut(int? id)
        {
            if (id.HasValue)
            {
                var employee = EmployeeServices.GetUser(id.Value);
                if (employee == null) throw new ArgumentException(string.Format("No employee found with an id = {0}", id.Value));

                SecurityServices.SignOut(employee, base.LocationId);
            }
            else
            {
                var signedIn = InvoiceServices.GetSignedInEmployees(base.LocationId, forceRefresh: true);
                foreach (var e in signedIn)
                {
                    SecurityServices.SignOut(e, base.LocationId);
                }
            }
            var employees = InvoiceServices.GetSignedInEmployees(base.LocationId, forceRefresh: true);

            return PartialView("_AvailableEmployees", employees);
        }
        #endregion
    }
}
