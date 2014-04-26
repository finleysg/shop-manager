using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class UserDetailModel
    {
        public UserModel User { get; set; }
        public SelectList Roles { get; set; }
        public string Action { get; set; }
        public UserFilterModel Filter { get; set; }
    }
}