using System.Collections.Generic;

namespace Enfield.ShopManager.Data.Graph
{
    public class AccountTypeService : AutoMapBase<AccountTypeService>
    {
        public virtual AccountType AccountType { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual IList<AccountTypeLabor> LaborTypeList { get; set; }
        public virtual decimal DefaultRate { get; set; }
        public virtual int DefaultEstimatedTime { get; set; }
        public virtual bool IsActive { get; set; }

        public AccountTypeService()
        {
            LaborTypeList = new List<AccountTypeLabor>();
        }

        public virtual void AddLabor(AccountTypeLabor child)
        {
            child.AccountTypeService = this;
            LaborTypeList.Add(child);
        }
    }
}
