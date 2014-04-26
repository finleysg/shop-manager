using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Enfield.ShopManager.Mapping
{
    public class LaborGraphToPayrollResolver : ValueResolver<Data.Graph.Labor, string>
    {
        protected override string ResolveCore(Data.Graph.Labor source)
        {
            if (source.Invoice == null)
                return null;
            else
                return source.Invoice.Location.Name;
        }
    }
}