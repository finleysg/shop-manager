using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Enfield.ShopManager.Filters;
using Enfield.ShopManager.Helpers;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Services;
using System.Web.Script.Serialization;
using Enfield.ShopManager.Security;

namespace Enfield.ShopManager.Controllers
{
    [BuildMenu()]
    [ValidateToken("Administrator")]
    public class AdministrationController : RestrictedControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("InvoiceListing");
        }

        #region | Invoice Administration |

        // TODO: test?
        public ActionResult InvoiceListing(InvoiceFilterModel filter)
        {
            var listing = AdministrationServices.GetInvoiceListing(filter);

            ViewBag.Filter = "InvoiceListing";
            ViewBag.Locations = LocationServices.GetLocationLookup(true, filter.LocationId);
            ViewBag.Paid = LookupServices.GetPaidOptions(filter.HasBeenPaid, true);
            ViewBag.Size = LookupServices.GetSizeOptions(filter.Size);

            if (listing.InvoiceList.Count == 0)
                return View("InvoiceListingNoData", listing);
            else
                return View("InvoiceListing", listing);
        }

        // TODO: test?
        public ActionResult InvoiceDetail(InvoiceFilterModel filter)
        {
            var detail = AdministrationServices.GetInvoiceAdministrationDetail(filter);

            ViewBag.Filter = "InvoiceDetail";
            ViewBag.Locations = LocationServices.GetLocationLookup(true, filter.LocationId);
            ViewBag.Paid = LookupServices.GetPaidOptions(filter.HasBeenPaid, true);

            if (detail.InvoiceList.Count == 0)
                return View("InvoiceDetailNoData", detail);
            else
                return View("InvoiceDetail", detail);
        }

        // TODO: test?
        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateInvoicePaidStatus(int id, bool isPaid)
        {
            //if (!isPaid) throw new Exception("I'm sorry Dave. I can't do that.");

            AdministrationServices.UpdateInvoicePaidStatus(id, isPaid);
            return Json(new { result = "ok" });
        }

        // TODO: test?
        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateInvoicePurchaseOrder(int id, string purchaseOrder)
        {
            AdministrationServices.UpdateInvoicePurchaseOrder(id, purchaseOrder);
            return Json(new { result = "ok" });
        }

        // TODO: test?
        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditServiceRate(int invoiceId, int serviceId, decimal rate)
        {
            var invoice = InvoiceServices.UpdateServiceRate(invoiceId, serviceId, rate);
            return PartialView("_ServiceList", invoice.ServiceList);
        }

        // TODO: test?
        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLaborRate(int invoiceId, int laborId, decimal rate)
        {
            var invoice = InvoiceServices.UpdateLaborRate(invoiceId, laborId, rate);
            return PartialView("_LaborList", invoice.LaborList);
        }

        #endregion

        #region | User Actions |

        // TODO: test?
        public ActionResult UserListing(UserFilterModel filter)
        {
            var listing = EmployeeServices.GetUserListing(filter);
            listing.Filter = filter;
            ViewBag.SizeOptions = LookupServices.GetSizeOptions(filter.Size);
            ViewBag.SiteAccessOptions = LookupServices.GetSiteAccessOptions(filter.HasSiteAccess, true);
            ViewBag.RoleOptions = LookupServices.GetRoleOptions(filter.Role, true);

            if (listing.UserList.Count == 0)
                return View("UserListingNoData", listing);
            else
                return View("UserListing", listing);
        }

        public ActionResult NewUser(UserFilterModel filter)
        {
            var model = new UserDetailModel()
            {
                Action = "NewUser",
                User = new UserModel(),
                Filter = filter,
                Roles = LookupServices.GetRoleOptions("Employee")
            };
            model.User.PasswordString = PasswordGenerator.GeneratePassword();

            ViewBag.Locations = LocationServices.GetLocationLookup(true, -1);
            return View("UserDetail", model);
        }

        [HttpPost]
        public ActionResult NewUser(UserModel user, UserFilterModel filter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeServices.CreateUser(user);
                    return RedirectToAction("UserListing", filter.GenerateUserAccessRoute());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
            }

            // Invalid - redisplay with errors
            var model = new UserDetailModel()
            {
                Action = "NewUser",
                User = user,
                Filter = filter,
                Roles = LookupServices.GetRoleOptions(user.RoleName)
            };

            ViewBag.Locations = LocationServices.GetLocationLookup(true, -1);
            return View("UserDetail", model);
        }

        public ActionResult EditUser(int id, UserFilterModel filter)
        {
            var user = EmployeeServices.GetUser(id);

            var model = new UserDetailModel()
            {
                Action = "EditUser",
                User = user,
                Filter = filter,
                Roles = LookupServices.GetRoleOptions(user.RoleName)
            };

            ViewBag.Locations = LocationServices.GetLocationLookup(true, model.User.LocationId);
            return View("UserDetail", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditUser(int id, UserFilterModel filter, FormCollection collection)
        {
            var user = EmployeeServices.GetUser(id);

            try
            {
                UpdateModel(user, "User");
                EmployeeServices.UpdateUser(user);

                return RedirectToAction("UserListing", filter.GenerateUserAccessRoute());
            }
            catch (Exception ex)
            {
                // Invalid - redisplay with errors
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
                var model = new UserDetailModel()
                {
                    Action = "EditUser",
                    User = user,
                    Filter = filter,
                    Roles = LookupServices.GetRoleOptions(user.RoleName)
                };

                ViewBag.Locations = LocationServices.GetLocationLookup(true, model.User.LocationId);
                return View("UserDetail", model);
            }
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ValidateUserName(FormCollection form)
        {
            var newName = form["User.Name"].ToString();
            var originalName = form["User.OriginalName"].ToString();

            //TODO: validate we have both names

            if (newName != originalName)
                return Json(EmployeeServices.IsUsernameUnique(newName));

            return Json(true);
        }

        [JsonErrorHandler(Order = 90)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult GeneratePassword()
        {
            return Json(PasswordGenerator.GeneratePassword(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region | Location Actions |

        // TODO: test?
        public ActionResult LocationListing()
        {
            var listing = LocationServices.GetLocationListing();
            return View("LocationListing", listing);
        }

        public ActionResult NewLocation()
        {
            ViewBag.Action = "NewLocation";
            return View("LocationDetail", new LocationModel());
        }

        [HttpPost]
        public ActionResult NewLocation(LocationModel location)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LocationServices.CreateLocation(location);
                    return RedirectToAction("LocationListing");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
            }

            ViewBag.Action = "NewLocation";
            return View("LocationDetail", location);
        }

        public ActionResult EditLocation(int id)
        {
            var location = LocationServices.GetLocation(id);
            ViewBag.Action = "EditLocation";

            return View("LocationDetail", location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLocation(int id, FormCollection collection)
        {
            var location = LocationServices.GetLocation(id);

            try
            {
                UpdateModel(location);
                LocationServices.UpdateLocation(location);

                return RedirectToAction("LocationListing");
            }
            catch (Exception ex)
            {
                // Invalid - redisplay with errors
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
                return View("LocationDetail", location);
            }
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ValidateLocationName(FormCollection form)
        {
            var newName = form["Name"].ToString();
            var originalName = form["OriginalName"].ToString();

            //TODO: validate we have both names

            if (newName != originalName)
            {
                var existing = LocationServices.GetLocationListing()
                    .Where(l => l.Name.Equals(newName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                return Json(existing == null);
            }

            return Json(true);
        }
        #endregion

        #region | Services and Labor |

        public ActionResult ServicesAndLabor(int? id)
        {
            var accountTypeId = (id.HasValue) ? id.Value : 1;

            var model = new ServicesAndLaborModel();
            model.AccountTypes = AccountTypeServices.GetServicesAndLabor().Where(t => t.Id == accountTypeId).ToList();

            ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(includeAll: false, selected: accountTypeId);
            ViewBag.LaborTypes = AccountTypeServices.GetLaborTypes();
            ViewBag.ServiceTypes = AccountTypeServices.GetServiceTypes();

            return View(model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateAccountType(string description)
        {
            AccountTypeServices.AddAccountType(description);
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateServiceType(string description)
        {
            AccountTypeServices.AddServiceType(description);
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateLaborType(string description)
        {
            AccountTypeServices.AddLaborType(description);
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateAccountType(int id, string description)
        {
            AccountTypeServices.AddAccountType(description);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateServiceType(int id, string description)
        {
            AccountTypeServices.UpdateServiceType(id, description);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateLaborType(int id, string description)
        {
            AccountTypeServices.UpdateLaborType(id, description);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddServiceToAccountType(int accountTypeId, int serviceTypeId)
        {
            AccountTypeServices.AddServiceToAccountType(accountTypeId, serviceTypeId);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddLaborToAccountType(int accountTypeId, int accountTypeServiceId, int laborTypeId)
        {
            AccountTypeServices.AddLaborToAccountType(accountTypeId, accountTypeServiceId, laborTypeId);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveServiceFromAccountType(int accountTypeId, int accountTypeServiceId)
        {
            AccountTypeServices.RemoveServiceFromAccountType(accountTypeId, accountTypeServiceId);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveLaborFromAccountType(int accountTypeId, int accountTypeServiceId, int accountTypeLaborId)
        {
            AccountTypeServices.RemoveLaborFromAccountType(accountTypeId, accountTypeServiceId, accountTypeLaborId);
            InvoiceServices.ClearAvailableTypeCache();
            return Json("ok");
        }
        #endregion

        public ActionResult SecurityLog(SecurityLogFilterModel filter)
        {
            var listing = SecurityServices.GetSecurityLog(filter);

            ViewBag.Locations = LocationServices.GetLocationLookup(true, filter.LocationId);
            ViewBag.Result = LookupServices.GetLoginResultOptions(filter.ResultFlag, true);
            ViewBag.Size = LookupServices.GetSizeOptions(filter.Size);

            if (listing.SecurityLog.Count == 0)
                return View("SecurityLogNoData", listing);
            else
                return View("SecurityLog", listing);
        }

        public ActionResult ErrorLog(string log)
        {
            var logs = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data")).GetFiles("*.log")
                .OrderByDescending(o => o.LastWriteTime)
                .Take(10)
                .ToDictionary(f => f.Name, f => f.FullName);
            string[] selected = { "Select a log file to view..." };

            if (!string.IsNullOrWhiteSpace(log))
            {
                var file = logs.Where(l => l.Key == log).FirstOrDefault();
                selected = System.IO.File.ReadAllLines(file.Value);
            }

            ErrorLogModel model = new ErrorLogModel() {
                LogFiles = logs.Select(l => l.Key).ToArray(),
                SelectedLog = selected
            };

            return View("ErrorLog", model);
        }
    }
}
