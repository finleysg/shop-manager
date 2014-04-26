using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Models;
using AutoMapper;

namespace Enfield.ShopManager.Mapping
{
    public class FilterToQuerySiteAccessResolver : ValueResolver<UserFilterModel, bool?>
    {
        protected override bool? ResolveCore(UserFilterModel source)
        {
            if (string.IsNullOrEmpty(source.HasSiteAccess) || source.HasSiteAccess == "All")
                return null;
            else
                return bool.Parse(source.HasSiteAccess);
        }
    }
}