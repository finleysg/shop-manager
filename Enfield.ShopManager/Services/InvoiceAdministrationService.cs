using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Enfield.ShopManager.Models;
using PagedList;
using System.Web.Caching;
using System;

namespace Enfield.ShopManager.Services
{
    public class InvoiceAdministrationService : DomainServiceBase
    {
        private IList<InvoiceViewModel> GetInvoices(InvoiceFilterModel filter)
        {
            var cacheKey = "invoice-admin";

            var invoices = HttpRuntime.Cache[cacheKey] as IList<InvoiceViewModel>;
            if (invoices == null || invoices.Count == 0 || filter.DoEvaluate)
            {
                // load from database
                Logger.Info("Loading invoice administration from database");
                var query = Mapper.Map<InvoiceFilterModel, Data.Query.InvoiceQuery>(filter);
                var result = InvoiceRepository.GetInvoices(query);
                invoices = Mapper.Map<IList<Data.Graph.InvoiceView>, IList<InvoiceViewModel>>(result);
                HttpRuntime.Cache.Insert(cacheKey, invoices, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1.0));
            }
            else
            {
                Logger.Info("Returning invoice administration from cache");
            }

            // load from cache unless told not to
            filter.DoEvaluate = false;

            return invoices;
        }

        public InvoiceListingModel GetInvoiceListing(InvoiceFilterModel filter)
        {
            if (filter == null)
            {
                filter = new InvoiceFilterModel();
            }

            var invoices = GetInvoices(filter);

            InvoiceListingModel model = new InvoiceListingModel();
            model.Filter = filter;
            model.InvoiceList = invoices.ToPagedList(filter.Page, filter.Size);

            for (int i = 0; i < model.InvoiceList.Count; i++)
                model.InvoiceList[i].RowId = ((filter.Page - 1) * filter.Size) + i + 1;

            return model;
        }

        public InvoiceAdministrationModel GetInvoiceAdministrationDetail(InvoiceFilterModel filter)
        {
            InvoiceAdministrationModel model = new InvoiceAdministrationModel(filter);

            if (filter == null)
            {
                filter = new InvoiceFilterModel();
                filter.Size = 1;
            }

            var invoices = GetInvoices(filter);
            if (invoices != null && invoices.Count > 0)
            {
                //falls back to first invoice if current page is off the end
                var currentId = GetCurrentInvoiceId(filter, invoices);

                model.InvoiceList = invoices.Select(i => i.Id).ToPagedList(filter.Page, filter.Size);
                model.CurrentInvoice = Mapper.Map<Data.Graph.Invoice, InvoiceModel>(InvoiceRepository.GetInvoice(currentId));
                model.Filter = filter;

                if (!string.IsNullOrEmpty(model.CurrentInvoice.StockNumber))
                {
                    var history = InvoiceRepository.GetInvoiceHistory(model.CurrentInvoice.StockNumber);
                    model.CurrentInvoice.History = Mapper.Map<IList<Data.Graph.StockNumberHistory>, List<HistoryModel>>(history);
                }
            }

            return model;
        }

        public InvoiceModel UpdateInvoicePaidStatus(int id, bool isPaid)
        {
            var invoice = InvoiceRepository.GetInvoice(id);
            invoice.IsPaid = isPaid;
            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Updating invoice {0} to paid = {1}", id, isPaid);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        public InvoiceModel UpdateInvoicePurchaseOrder(int id, string poNumber)
        {
            var invoice = InvoiceRepository.GetInvoice(id);
            invoice.PurchaseOrderNumber = poNumber;
            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Updating invoice {0} PO number to {1}", id, poNumber);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        private int GetCurrentInvoiceId(InvoiceFilterModel filter, IList<InvoiceViewModel> invoices)
        {
            //fall back to first invoice if current page is off the end
            int currentId;
            if (filter.Page - 1 >= invoices.Count)
            {
                currentId = invoices[0].Id;
                filter.Page = 1;
            }
            else
            {
                currentId = invoices[filter.Page - 1].Id;
            }
            return currentId;
        }

    }
}