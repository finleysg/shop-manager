using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class DailyLogModel
    {
        public string LocationName { get; set; }
        public List<InvoiceModel> Invoices { get; set; }
        public string Count
        {
            get { return Invoices.Count.ToString(); }
        }
    }
}