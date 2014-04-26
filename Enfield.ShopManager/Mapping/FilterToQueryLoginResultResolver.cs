using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Enfield.ShopManager.Models;

namespace Enfield.ShopManager.Mapping
{
    public class FilterToQueryLoginResultResolver : ValueResolver<SecurityLogFilterModel, bool?>
    {
        protected override bool? ResolveCore(SecurityLogFilterModel source)
        {
            if (string.IsNullOrEmpty(source.ResultFlag) || source.ResultFlag == "All")
                return null;
            else
                return (source.ResultFlag == "Success") ? true : false;
        }
    }
}