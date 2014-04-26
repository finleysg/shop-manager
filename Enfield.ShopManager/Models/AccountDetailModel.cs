using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class AccountDetailModel
    {
        public AccountModel Account { get; set; }
        public string Action { get; set; }
        public AccountFilterModel Filter { get; set; }
    }
}