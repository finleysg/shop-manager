using System;
using System.Collections.Generic;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Data.Query
{
    public class InvoiceQuery : QueryBase
    {
        public bool? HasBeenPaid { get; set; }
        public bool? HadBeenCompleted { get; set; }
        public int? LocationId { get; set; }
        public string AccountName { get; set; }
        public int[] AccountIds { get; set; }
        public DateTime? ReceivedDateStart { get; set; }
        public DateTime? ReceivedDateEnd { get; set; }
        public DateTime? CompletedDateStart { get; set; }
        public DateTime? CompletedDateEnd { get; set; }
        public string StockNumber { get; set; }
        public bool ExcludeZeroTotal { get; set; }
    }
}
