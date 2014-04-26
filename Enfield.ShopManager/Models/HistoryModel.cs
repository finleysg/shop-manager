using System;

namespace Enfield.ShopManager.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public string StockNumber { get; set; }
        public int InvoiceId { get; set; }
        public string Note { get; set; }
        public string ModifyUser { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}