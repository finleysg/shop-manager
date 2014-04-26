using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Models;
using AutoMapper;

namespace Enfield.ShopManager.Mapping
{
    public class FilterToQueryPaidResolver : ValueResolver<InvoiceFilterModel, bool?>
    {
        protected override bool? ResolveCore(InvoiceFilterModel source)
        {
            if (string.IsNullOrEmpty(source.HasBeenPaid) || source.HasBeenPaid == "All")
                return null;
            else
                return (source.HasBeenPaid == "Paid") ? true : false;
        }
    }
}