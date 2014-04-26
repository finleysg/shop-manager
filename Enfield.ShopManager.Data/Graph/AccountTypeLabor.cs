namespace Enfield.ShopManager.Data.Graph
{
    public class AccountTypeLabor : AutoMapBase<AccountTypeLabor>
    {
        public virtual AccountTypeService AccountTypeService { get; set; }
        public virtual LaborType LaborType { get; set; }
        public virtual decimal DefaultRate { get; set; }
        public virtual string DefaultRateType { get; set; }
    }
}
