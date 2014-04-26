using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Enfield.ShopManager.Models
{
    public class AccountListingModel
    {
        public AccountFilterModel Filter { get; set; }
        public IPagedList<AccountModel> AccountList { get; set; }
    }
}