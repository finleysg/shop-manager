using System;
using System.Collections.Generic;
using System.Linq;
using Enfield.ShopManager.Data.Map;


namespace Enfield.ShopManager.Data.Graph
{
    public class InvoiceView : AutoMapBase<InvoiceView>
    {
        public virtual int LocationId { get; set; }
        public virtual string LocationName { get; set; }
        public virtual int AccountId { get; set; }
        public virtual string AccountName { get; set; }
        public virtual DateTime ReceiveDate { get; set; }
        public virtual DateTime? CompleteDate { get; set; }
        public virtual string VIN { get; set; }
        public virtual string StockNumber { get; set; }
        public virtual string Year { get; set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual string Color { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual bool IsPaid { get; set; }
        public virtual string PurchaseOrderNumber { get; set; }
        public virtual decimal Total { get; set; }
        public virtual decimal Tax { get; set; }
    }
}
