using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enfield.ShopManager.Data.Graph;
using NHibernate.Criterion;
using Enfield.ShopManager.Data.Query;

namespace Enfield.ShopManager.Data.Repository
{
    public class ReportRepository : RepositoryBase, IRepository
    {
        public IList<OpenBalanceView> GetAccountsWithOpenBalances(string accountType)
        {
            var criteria = Session.CreateCriteria<OpenBalanceView>();
            criteria.Add(Expression.Eq("AccountType", accountType));
            return criteria.List<OpenBalanceView>();
        }

        public IList<Labor> GetLabor(LaborQuery query)
        {
            var criteria = Session.CreateCriteria<Labor>();
            criteria.Add(Expression.Between("LaborDate", query.StartDate, query.EndDate));
            if (query.EmployeeIds.Count > 0)
            {
                criteria.CreateAlias("Employee", "e").Add(Expression.In("e.Id", query.EmployeeIds));
            }

            criteria.AddOrder(new Order("ModifyDate", true));

            var result = criteria.List<Labor>();
            if (query.LocationId.HasValue)
            {
                return result.Where(r => r.Invoice.Location.Id == query.LocationId.Value).ToList();
            }
            return result;
        }

        public IList<ServiceTotalsView> GetServiceTotals(DateTime startDate, DateTime endDate, int? locationId)
        {
            var criteria = Session.CreateCriteria<ServiceTotalsView>();
            criteria.Add(Expression.Between("CompleteDate", startDate, endDate));
            if (locationId.HasValue) criteria.Add(Expression.Eq("LocationId", locationId.Value));

            criteria.AddOrder(new Order("AccountName", true));
            criteria.AddOrder(new Order("CompleteDate", true));

            return criteria.List<ServiceTotalsView>();
        }

        public IList<Invoice> GetDailyLog(DateTime logDate, int? locationId)
        {
            var criteria = Session.CreateCriteria<Invoice>();
            criteria.Add(Expression.Between("ReceiveDate", logDate, logDate.AddDays(1)));
            if (locationId.HasValue) criteria.CreateAlias("Location", "l").Add(Expression.Eq("l.Id", locationId.Value));

            return criteria.List<Invoice>();
        }

        public IList<EmployeeLogView> GetEmployeeLog(SignInQuery query)
        {
            var criteria = Session.CreateCriteria<EmployeeLogView>();
            criteria.Add(Expression.Between("SignInDate", query.SignInDateStart.Value, query.SignInDateEnd.Value));
            if (query.LocationId.HasValue) criteria.CreateAlias("Location", "l").Add(Expression.Eq("l.Id", query.LocationId.Value));

            return criteria.List<EmployeeLogView>();
        }

    }
}
