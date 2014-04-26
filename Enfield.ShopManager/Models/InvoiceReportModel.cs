using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class InvoiceReportModel
    {
        public InvoiceModel Invoice { get; set; }
        public AccountModel Account { get; set; }
        public ReportHeaderModel Header { get; set; }

        private decimal Subtotal 
        {
            get { return Invoice.ServiceList.Sum(s => s.Rate); }
        }

        private decimal Tax
        {
            get { return Subtotal * Invoice.TaxRate; }
        }

        public string FormattedSubtotal
        { 
            get { return Subtotal.ToString("C2"); }
        }

        public string FormattedTax
        {
            get { return Tax.ToString("C2"); }
        }

        public string FormattedAmountDue
        { 
            get { return (Subtotal + Tax).ToString("C2"); }
        }
    }
}