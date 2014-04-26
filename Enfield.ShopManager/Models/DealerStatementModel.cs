using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class DealerStatementModel
    {
        public AccountModel Account { get; set; }
        public List<InvoiceViewModel> Invoices { get; set; }

        public string FormattedTotal
        {
            get
            {
                return Invoices.Sum(i => i.Total).ToString("C2");
            }
        }

        public string FormattedGrandTotal
        {
            get
            {
                return Invoices.Sum(i => i.Total + i.Tax).ToString("C2");
            }
        }
    }
}