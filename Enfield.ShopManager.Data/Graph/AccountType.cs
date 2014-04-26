using System;
using System.Collections.Generic;

namespace Enfield.ShopManager.Data.Graph
{
    public class AccountType : AutoMapBase<AccountType>
    {
        public virtual IList<AccountTypeService> ServiceTypeList { get; set; }
        public virtual double TaxRate { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }

        private string description;
        public virtual string Description
        {
            get { return description; }
            set { description = (value == null) ? null : value.ToUpper(); }
        }

        public AccountType()
        {
            ServiceTypeList = new List<AccountTypeService>();
        }

        public AccountType(int id)
        {
            base.Id = id;
            ServiceTypeList = new List<AccountTypeService>();
        }

        public virtual void AddService(AccountTypeService child)
        {
            child.AccountType = this;
            ServiceTypeList.Add(child);
        }
    }
}
