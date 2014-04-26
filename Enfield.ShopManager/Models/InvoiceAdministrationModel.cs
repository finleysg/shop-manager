using PagedList;
using System.Collections.Generic;

namespace Enfield.ShopManager.Models
{
    public class InvoiceAdministrationModel
    {
        public InvoiceFilterModel Filter { get; set; }
        public InvoiceModel CurrentInvoice { get; set; }
        public IPagedList<int> InvoiceList { get; set; }

        public InvoiceAdministrationModel()
        {
            Filter = new InvoiceFilterModel() { Size = 1 };
            InvoiceList = new PagedList<int>(new List<int>(), Filter.Page, Filter.Size);
            CurrentInvoice = new InvoiceModel();
        }

        public InvoiceAdministrationModel(InvoiceFilterModel filter)
        {
            Filter = filter;
            InvoiceList = new PagedList<int>(new List<int>(), Filter.Page, Filter.Size);
            CurrentInvoice = new InvoiceModel();
        }
    }
}