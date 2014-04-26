using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class UserListingModel
    {
        public UserFilterModel Filter { get; set; }
        public IPagedList<UserModel> UserList { get; set; }
    }
}