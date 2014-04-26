using Enfield.ShopManager.Models;

namespace Enfield.ShopManager.Services
{
    public interface IInvoiceService
    {
        InvoiceAdministrationModel GetInvoiceAdministrationDetail(InvoiceFilterModel filter);
        InvoiceListingModel GetInvoiceListing(InvoiceFilterModel filter);
    }
}
