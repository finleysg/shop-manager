using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Query
{
    public class UserQuery
    {
        public bool? HasSiteAccess { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }

        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
