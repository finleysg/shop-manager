using System;
using System.Collections.Generic;
using System.Linq;
using Enfield.ShopManager.Data.Graph;
using Enfield.ShopManager.Data.Query;
using NHibernate.Criterion;

namespace Enfield.ShopManager.Data.Repository
{
    public class InvoiceRepository : RepositoryBase, IRepository
    {

        public Invoice GetInvoice(int id)
        {
            var invoice = Session.QueryOver<Invoice>()
                .Where(i => i.Id == id).SingleOrDefault();
            return invoice;
        }

        public IList<InvoiceView> GetInvoices(InvoiceQuery query)
        {
            var criteria = GetInvoiceFilterCriteria(query);
            var result = criteria.List<InvoiceView>();
            return result;
        }

        protected NHibernate.ICriteria GetInvoiceFilterCriteria(InvoiceQuery query, bool isCount = false)
        {
            var criteria = Session.CreateCriteria<InvoiceView>();

            if (!string.IsNullOrEmpty(query.AccountName)) criteria.Add(Expression.Like("AccountName", query.AccountName + "%"));
            if (query.AccountIds != null && query.AccountIds.Length > 0) criteria.Add(Expression.In("AccountId", query.AccountIds));
            if (query.LocationId.HasValue) criteria.Add(Expression.Eq("LocationId", query.LocationId.Value));
            if (query.ReceivedDateStart.HasValue) criteria.Add(Expression.Ge("ReceiveDate", query.ReceivedDateStart.Value));
            if (query.ReceivedDateEnd.HasValue) criteria.Add(Expression.Le("ReceiveDate", query.ReceivedDateEnd.Value));
            if (query.CompletedDateStart.HasValue) criteria.Add(Expression.Ge("CompleteDate", query.CompletedDateStart.Value));
            if (query.CompletedDateEnd.HasValue) criteria.Add(Expression.Le("CompleteDate", query.CompletedDateEnd.Value));
            if (!string.IsNullOrEmpty(query.StockNumber)) criteria.Add(Expression.Like("StockNumber", query.StockNumber + "%"));
            if (!string.IsNullOrEmpty(query.VIN)) criteria.Add(Expression.Like("VIN", "%" + query.VIN + "%"));
            if (query.HadBeenCompleted.HasValue) criteria.Add(Expression.Eq("IsComplete", query.HadBeenCompleted.Value));
            if (query.HasBeenPaid.HasValue) criteria.Add(Expression.Eq("IsPaid", query.HasBeenPaid.Value));
            if (query.ExcludeZeroTotal) criteria.Add(Expression.Gt("Total", decimal.Parse("0.0")));

            if (string.IsNullOrEmpty(query.SortBy))
                criteria.AddOrder(new Order("Id", true));
            else
                criteria.AddOrder(new Order(query.SortBy, (query.SortDirection != null && query.SortDirection.Equals("asc", StringComparison.InvariantCultureIgnoreCase))));

            return criteria;
        }

        public IList<StockNumberHistory> GetInvoiceHistory(string vin)
        {
            var criteria = Session.CreateCriteria<StockNumberHistory>();
            criteria.Add(Expression.Eq("StockNumber", vin));
            criteria.AddOrder(new Order("ModifyDate", true));

            return criteria.List<StockNumberHistory>();
        }

        public void SaveHistory(StockNumberHistory history)
        {
            Session.Save(history);
        }

        public Invoice SaveInvoice(Invoice invoice)
        {
            Session.SaveOrUpdate(invoice);
            return invoice;
        }

        public void DeleteInvoice(Invoice invoice)
        {
            Session.Delete(invoice);
        }

    }
}
