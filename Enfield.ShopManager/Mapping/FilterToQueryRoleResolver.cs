using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Enfield.ShopManager.Models;

namespace Enfield.ShopManager.Mapping
{
    public class FilterToQueryRoleResolver : ValueResolver<UserFilterModel, string>
    {
        protected override string ResolveCore(UserFilterModel source)
        {
            if (string.IsNullOrEmpty(source.Role) || source.Role == "All")
                return null;
            else
                return source.Role;
        }
    }
}