using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Query
{
    public abstract class QueryBase
    {
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
