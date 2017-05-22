using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Data.Query;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Data.Graph;
using AutoMapper;
using NHibernate.Criterion;

namespace Enfield.ShopManager.Services
{
    public class ReportService : DomainServiceBase
    {
        public LocationService LocationServices { get; set; }

        public List<DailyLogModel> GetDailyLog(DateTime logDate, string role, int locationId)
        {
            int? locationFilter = null;
            if (role.Equals("Manager", StringComparison.InvariantCultureIgnoreCase)) locationFilter = locationId;

            var invoices = ReportRepository.GetDailyLog(logDate, locationFilter)
                .GroupBy(i => i.Location.Name)
                .OrderBy(o => o.Key);

            List<DailyLogModel> report = new List<DailyLogModel>();
            foreach (IGrouping<string, Invoice> log in invoices)
            {
                report.Add(new DailyLogModel()
                {
                    LocationName = log.Key,
                    Invoices = Mapper.Map<IList<Data.Graph.Invoice>, List<Models.InvoiceModel>>(log.ToList())
                });
            }

            return report;
        }

        public List<DealerStatementModel> GetStatements(DateTime endDate, List<int> accountIds, string role, int locationId)
        {
            InvoiceQuery query = new InvoiceQuery();
            query.ExcludeZeroTotal = true;
            query.HadBeenCompleted = true;
            query.HasBeenPaid = false;
            if (accountIds.Count > 0) query.AccountIds = accountIds.ToArray();
            query.ReceivedDateEnd = endDate;
            query.SortBy = "Id";
            query.SortDirection = "Asc";
            if (role.Equals("Manager", StringComparison.InvariantCultureIgnoreCase)) query.LocationId = locationId;
            
            var reportData = InvoiceRepository.GetInvoices(query)
                    .Where(i => i.Total > 0)
                    .GroupBy(i => i.AccountId)
                    .OrderBy(a => a.Key);

            List<DealerStatementModel> report = new List<DealerStatementModel>();
            foreach (IGrouping<int, InvoiceView> statement in reportData)
            {
                var account = AccountRepository.GetAccount(statement.Key);
                report.Add(new DealerStatementModel()
                {
                    Account = Mapper.Map<Data.Graph.Account, AccountModel>(account),
                    Invoices = Mapper.Map<IList<Data.Graph.InvoiceView>, List<InvoiceViewModel>>(statement.ToList())
                });
            }

            return report.OrderBy(o => o.Account.Name).ToList();
        }

        public List<DealerTotalModel> GetServiceTotals(DateTime startDate, DateTime endDate, string role, int locationId)
        {
            int? locationFilter = null;
            if (role.Equals("Manager", StringComparison.InvariantCultureIgnoreCase)) locationFilter = locationId;

            var reportData1 = ReportRepository.GetServiceTotals(startDate, endDate, locationFilter).Distinct();
            var reportData = reportData1.GroupBy(t => new { t.LocationId, t.AccountName });

            List<DealerTotalModel> report = new List<DealerTotalModel>();
            foreach (var total in reportData)
            {
                var location = LocationServices.GetLocation(total.Key.LocationId);
                report.Add(new DealerTotalModel()
                {
                    AccountName = string.Format("{0} ({1})", total.Key.AccountName, location.Name),
                    ServicesByDate = Mapper.Map<IList<Data.Graph.ServiceTotalsView>, List<ServiceTotalViewModel>>(total.ToList())
                });
            }

            return report;
        }

        public List<PayrollStatementModel> GetPayrollStatements(DateTime startDate, DateTime endDate, List<int> employeeIds, string role, int locationId)
        {
            int? locationFilter = null;
            if (role.Equals("Manager", StringComparison.InvariantCultureIgnoreCase)) locationFilter = locationId;

            var reportData = ReportRepository.GetLabor(
                new LaborQuery() { StartDate = startDate, EndDate = endDate, EmployeeIds = employeeIds, LocationId = locationFilter })
                .GroupBy(r => r.Employee);

            List<PayrollStatementModel> report = new List<PayrollStatementModel>();
            foreach (var statement in reportData)
            {
                report.Add(new PayrollStatementModel()
                {
                    Employee = Mapper.Map<Data.Graph.Employee, EmployeeModel>(statement.Key),
                    Labor = Mapper.Map<IList<Data.Graph.Labor>, List<PayrollModel>>(statement.ToList())
                });
            }

            return report.OrderBy(o => o.Employee.DisplayName).ToList();
        }

        public List<EmployeeLogModel> GetEmployeeLog(DateTime startDate, DateTime endDate, string role, int locationId)
        {
            int? locationFilter = null;
            if (role.Equals("Manager", StringComparison.InvariantCultureIgnoreCase)) locationFilter = locationId;

            var reportData = ReportRepository.GetEmployeeLog(
                new SignInQuery() { SignInDateStart = startDate, SignInDateEnd = endDate, LocationId = locationFilter })
                .GroupBy(r => r.SignInDate.ToShortDateString());

            List<EmployeeLogModel> report = new List<EmployeeLogModel>();
            foreach (var item in reportData)
            {
                report.Add(new EmployeeLogModel()
                {
                    WorkDate = item.Key,
                    EmployeeList = Mapper.Map<IList<Data.Graph.EmployeeLogView>, List<EmployeeSignInModel>>(item.OrderBy(o => o.Location.Name).ThenBy(e => e.Name).ToList())
                });
            }

            return report.OrderBy(o => o.WorkDate).ToList();
        }
    }
}