namespace Enfield.ShopManager.Data.Graph
{
    public class OpenBalanceView : AutoMapBase<OpenBalanceView>
    {
        public virtual int AccountId { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string AccountType { get; set; }
        public virtual decimal BalanceDue { get; set; }
    }
}
