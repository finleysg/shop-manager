using PagedList;

namespace Enfield.ShopManager.Models
{
    public class InvoiceListingModel
    {
        public InvoiceFilterModel Filter { get; set; }
        public IPagedList<InvoiceViewModel> InvoiceList { get; set; }
    }
}