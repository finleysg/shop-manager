namespace Enfield.ShopManager.Data.Graph
{
    public class InvoiceType : AutoMapBase
    {
        public virtual int InvoiceTypeId { get; set; }
        public virtual string Description { get; set; }
    }
}
