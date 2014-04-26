using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Enfield.ShopManager.Models;

namespace Enfield.ShopManager.Mapping
{
    public class FilterToQueryLocationResolver : ValueResolver<InvoiceFilterModel, int?>
    {
        protected override int? ResolveCore(InvoiceFilterModel source)
        {
            if (source.LocationId.HasValue && source.LocationId.Value == -1)
                return null;
            else
                return source.LocationId;
        }
    }
}