using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class StockNumberHistory : AutoMapBase<StockNumberHistory>, Audit.IHaveAuditInformation
    {
        private string stockNumber;
        public virtual string StockNumber 
        {
            get { return stockNumber; }
            set { stockNumber = (value == null) ? null : value.ToUpper(); }
        }
        public virtual int InvoiceId { get; set; }
        public virtual string Note { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }
    }
}
