using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Enfield.ShopManager.Models
{
    public class SecurityLogListingModel
    {
        public SecurityLogFilterModel Filter { get; set; }
        public IPagedList<SecurityLogModel> SecurityLog { get; set; }
    }
}