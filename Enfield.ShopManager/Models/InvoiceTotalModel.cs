using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class InvoiceTotalModel
    {
        public int RowId { get; set; }
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string AccountName { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string StockNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal ServiceTotal { get; set; }
        public bool IsPaid { get; set; }
    }
}