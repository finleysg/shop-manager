using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Enfield.ShopManager.Data.Repository;
using Enfield.ShopManager.Security;
using Enfield.ShopManager.Filters;
using Enfield.ShopManager.Models;
using System.Text;

namespace Enfield.ShopManager.Controllers
{
    [BuildMenu()]
    [ValidateToken("Employee,Manager,Administrator")]
    public class ReportingController : RestrictedControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("Invoice");
        }

        #region | Daily Log Report |

        public ActionResult DailyLog()
        {
            DailyLogParamModel model = new DailyLogParamModel();
            model.LogDate = DateTime.Today.ToShortDateString();

            return View("DailyLog", model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DailyLog(string logDate)
        {
            DateTime start;
            if (!DateTime.TryParse(logDate, out start))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", logDate), "logDate");
            }

            var reportUrl = Url.Action("DailyLogReport", new { start = start.ToString("yyyy-MM-dd") });
            return Json(reportUrl);
        }

        public ActionResult DailyLogReport(DateTime start)
        {
            DailyLogReport model = new Models.DailyLogReport();
            model.Invoices = ReportServices.GetDailyLog(start, base.SecurityToken.RoleName, base.LocationId);
            model.Header = new ReportHeaderModel("Daily Log");
            model.StartDate = start.ToShortDateString();

            return View(model);
        }

        public ActionResult DailyLogExcel(DateTime start)
        {
            var model = new DailyLogReport();
            model.Invoices = ReportServices.GetDailyLog(start, base.SecurityToken.RoleName, base.LocationId);
            model.Header = new ReportHeaderModel("Daily Log");
            model.StartDate = start.ToShortDateString();

            Response.AddHeader("Content-Disposition", "filename=DailyLog.xlsx");
            Response.ContentType = "application/vnd.ms-excel";

            return View("DailyLogReport", model);
        }
        #endregion

        #region | Dealer Statement Report |

        public ActionResult DealerStatements()
        {
            DealerStatementParamModel model = new DealerStatementParamModel();
            model.DealerAccounts = AccountServices.GetAccountLookupWithBalanceDue("DEALER");
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();

            return View("DealerStatements", model);
        }

        [JsonErrorHandler(Order=90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DealerStatements(string endDate, string reportType, string[] selectedAccounts)
        {
            DateTime end;
            if (!DateTime.TryParse(endDate, out end))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", endDate), "endDate");
            }

            var action = (reportType == "data-only") ? "Export" : "Report";
            var reportUrl = Url.Action("DealerStatements" + action, 
                new { 
                    end = end.ToString("yyyy-MM-dd"), 
                    idList = ConcatenateIdList(selectedAccounts) 
                });

            return Json(reportUrl);
        }

        public ActionResult DealerStatementsReport(DateTime end, string idList)
        {
            int[] dealers = ParseAccountIdList(idList);

            DealerStatementsReport model = new DealerStatementsReport();
            model.Statements = ReportServices.GetStatements(end, new List<int>(dealers), base.SecurityToken.RoleName, base.LocationId);
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Account Statement");

            return View(model);
        }

        public ActionResult DealerStatementsExport(DateTime end, string idList)
        {
            int[] dealers = ParseAccountIdList(idList);

            DealerStatementsReport model = new DealerStatementsReport();
            model.Statements = ReportServices.GetStatements(end, new List<int>(dealers), base.SecurityToken.RoleName, base.LocationId);
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Account Statement");

            return View(model);
        }
        #endregion

        #region | Dealer Totals Report |

        public ActionResult DealerTotals()
        {
            DealerTotalsParamModel model = new DealerTotalsParamModel();
            model.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();

            return View("DealerTotals", model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DealerTotals(string startDate, string endDate, string reportType)
        {
            DateTime start;
            if (!DateTime.TryParse(startDate, out start))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", startDate), "startDate");
            }

            DateTime end;
            if (!DateTime.TryParse(endDate, out end))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", endDate), "endDate");
            }
            var action = (reportType == "data-only") ? "Export" : "Report";
            var reportUrl = Url.Action("DealerTotals" + action, new { start = start.ToString("yyyy-MM-dd"), end = end.ToString("yyyy-MM-dd") });
            return Json(reportUrl);
        }

        public ActionResult DealerTotalsReport(DateTime start, DateTime end)
        {
            DealerTotalsReport model = new DealerTotalsReport();
            model.Totals = ReportServices.GetServiceTotals(start, end, base.SecurityToken.RoleName, base.LocationId);
            model.StartDate = start.ToShortDateString();
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Dealer Totals");

            return View(model);
        }

        public ActionResult DealerTotalsExport(DateTime start, DateTime end)
        {
            DealerTotalsReport model = new DealerTotalsReport();
            model.Totals = ReportServices.GetServiceTotals(start, end, base.SecurityToken.RoleName, base.LocationId);
            model.StartDate = start.ToShortDateString();
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Dealer Totals");

            return View(model);
        }
        #endregion

        #region | Employee Log Report |

        public ActionResult EmployeeLog()
        {
            PayrollParamModel model = new PayrollParamModel();
            model.StartDate = DateTime.Today.AddDays(-7).ToShortDateString();
            model.EndDate = DateTime.Today.ToShortDateString();

            return View("EmployeeLog", model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeeLog(string startDate, string endDate, string reportType)
        {
            DateTime start;
            if (!DateTime.TryParse(startDate, out start))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", startDate), "startDate");
            }

            DateTime end;
            if (!DateTime.TryParse(endDate, out end))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", endDate), "endDate");
            }
            var action = (reportType == "data-only") ? "Export" : "Report";
            var reportUrl = Url.Action("EmployeeLog" + action, new { start = start.ToString("yyyy-MM-dd"), end = end.ToString("yyyy-MM-dd") });
            return Json(reportUrl);
        }

        public ActionResult EmployeeLogReport(DateTime start, DateTime end)
        {
            EmployeeLogReport model = new EmployeeLogReport();
            model.Employees = ReportServices.GetEmployeeLog(start, end, base.SecurityToken.RoleName, base.LocationId);
            model.StartDate = start.ToShortDateString();
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Employee Log");

            return View(model);
        }

        public ActionResult EmployeeLogExport(DateTime start, DateTime end)
        {
            EmployeeLogReport model = new EmployeeLogReport();
            model.Employees = ReportServices.GetEmployeeLog(start, end, base.SecurityToken.RoleName, base.LocationId);
            model.StartDate = start.ToShortDateString();
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Dealer Totals");

            return View(model);
        }
        #endregion

        #region | Invoice Report |

        public ActionResult Invoice()
        {
            var invoice = NavigationServices.GetVehiclesInShop(base.LocationId).FirstOrDefault();
            InvoiceParamModel model = new InvoiceParamModel();
            model.InvoiceId = (invoice == null) ? string.Empty : invoice.Id.ToString();

            return View("Invoice", model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InvoiceUrl(string invoiceId)
        {
            int id;
            if (!int.TryParse(invoiceId, out id))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to an invoice number", invoiceId), "invoiceId");
            }

            var reportUrl = Url.Action("InvoiceReport", new { id = id });
            return Json(reportUrl);
        }

        public ActionResult InvoiceReport(int id)
        {
            InvoiceReportModel model = new InvoiceReportModel();
            model.Invoice = InvoiceServices.GetInvoice(id);
            model.Account = AccountServices.GetAccount(model.Invoice.AccountName);
            model.Header = new ReportHeaderModel("Invoice");

            return View(model);
        }
        #endregion

        #region | Private Statement Report |

        public ActionResult PrivateStatements()
        {
            DealerStatementParamModel model = new DealerStatementParamModel();
            model.DealerAccounts = AccountServices.GetAccountLookupWithBalanceDue("PRIVATE");
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();

            return View("PrivateStatements", model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrivateStatements(string endDate, string reportType, string[] selectedAccounts)
        {
            DateTime end;
            if (!DateTime.TryParse(endDate, out end))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", endDate), "endDate");
            }

            var action = (reportType == "data-only") ? "Export" : "Report";
            var reportUrl = Url.Action("PrivateStatements" + action,
                new
                {
                    end = end.ToString("yyyy-MM-dd"),
                    idList = ConcatenateIdList(selectedAccounts)
                });

            return Json(reportUrl);
        }

        public ActionResult PrivateStatementsReport(DateTime end, string idList)
        {
            int[] accounts = ParseAccountIdList(idList);

            DealerStatementsReport model = new DealerStatementsReport();
            model.Statements = ReportServices.GetStatements(end, new List<int>(accounts), base.SecurityToken.RoleName, base.LocationId);
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Account Statement");

            return View(model);
        }

        public ActionResult PrivateStatementsExport(DateTime end, string idList)
        {
            int[] accounts = ParseAccountIdList(idList);

            DealerStatementsReport model = new DealerStatementsReport();
            model.Statements = ReportServices.GetStatements(end, new List<int>(accounts), base.SecurityToken.RoleName, base.LocationId);
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Account Statement");

            return View(model);
        }
        #endregion

        #region | Payroll Report |

        public ActionResult Payroll()
        {
            PayrollParamModel model = new PayrollParamModel();
            model.ActiveEmployees = EmployeeServices.GetActiveEmployees();
            model.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();

            return View("Payroll", model);
        }

        [JsonErrorHandler(Order = 90)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Payroll(string startDate, string endDate, string reportType, string[] selectedEmployees)
        {
            DateTime start;
            if (!DateTime.TryParse(startDate, out start))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", startDate), "startDate");
            }

            DateTime end;
            if (!DateTime.TryParse(endDate, out end))
            {
                throw new ArgumentException(string.Format("{0} cannot be converted to a date", endDate), "endDate");
            }

            var action = (reportType == "data-only") ? "Export" : "Report";
            var reportUrl = Url.Action("Payroll" + action, 
                new { 
                    start = start.ToString("yyyy-MM-dd"), 
                    end = end.ToString("yyyy-MM-dd"), 
                    idList = ConcatenateIdList(selectedEmployees)
                });

            return Json(reportUrl);
        }

        public ActionResult PayrollReport(DateTime start, DateTime end, string idList)
        {
            int[] employees = ParseEmployeeIdList(idList);

            PayrollStatementsReport model = new PayrollStatementsReport();
            model.Statements = ReportServices.GetPayrollStatements(start, end, employees.ToList(), base.SecurityToken.RoleName, base.LocationId);
            model.StartDate = start.ToShortDateString();
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Payroll Statements");

            return View(model);
        }

        public ActionResult PayrollExport(DateTime start, DateTime end, string idList)
        {
            int[] employees = ParseEmployeeIdList(idList);

            PayrollStatementsReport model = new PayrollStatementsReport();
            model.Statements = ReportServices.GetPayrollStatements(start, end, employees.ToList(), base.SecurityToken.RoleName, base.LocationId);
            model.StartDate = start.ToShortDateString();
            model.EndDate = end.ToShortDateString();
            model.Header = new ReportHeaderModel("Payroll Statements");

            return View(model);
        }
        #endregion

        private string ConcatenateIdList(string[] idList)
        {
            StringBuilder sb = new StringBuilder();
            if (idList != null)
            {
                foreach (string id in idList)
                {
                    sb.AppendFormat("{0}-", id);
                }
                sb.Remove(sb.ToString().Length - 1, 1);
            }
            return sb.ToString();
        }

        private int[] ParseAccountIdList(string idList)
        {
            if (string.IsNullOrEmpty(idList))
            {
                //return AccountServices.GetAccountListing(
                //    new AccountFilterModel() { AccountTypeId = 1 }).AccountList.Select(a => a.Id).ToArray();
                return new int[0];
            }
            else
            {
                return (from i in idList.Split("-".ToCharArray())
                        select int.Parse(i)).ToArray();
            }
        }

        private int[] ParseEmployeeIdList(string idList)
        {
            if (string.IsNullOrEmpty(idList))
            {
                return EmployeeServices.GetUserListing(
                    new UserFilterModel() { HasSiteAccess = "true" }).UserList.Select(u => u.Id).ToArray();
            }
            else
            {
                return (from i in idList.Split("-".ToCharArray())
                        select int.Parse(i)).ToArray();
            }
        }
    }
}
