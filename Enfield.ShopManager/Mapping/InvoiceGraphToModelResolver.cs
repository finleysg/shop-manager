using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Enfield.ShopManager.Mapping
{
    public class InvoiceGraphToModelResolver : ValueResolver<Data.Graph.Invoice, string>
    {
        protected override string ResolveCore(Data.Graph.Invoice source)
        {
            if (source.Account == null)
                return null;
            else
                return source.Account.AccountType.Description;
        }
    }
}