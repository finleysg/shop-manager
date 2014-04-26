using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Enfield.ShopManager.Models;

namespace Enfield.ShopManager.Mapping
{
    public class FilterToQueryAccountTypeResolver : ValueResolver<AccountFilterModel, int?>
    {
        protected override int? ResolveCore(AccountFilterModel source)
        {
            if (source.AccountTypeId.HasValue && source.AccountTypeId.Value == -1)
                return null;
            else
                return source.AccountTypeId;
        }
    }
}