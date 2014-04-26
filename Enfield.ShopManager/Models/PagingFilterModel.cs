using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public abstract class PagingFilterModel
    {
        public PagingFilterModel()
        {
            Page = 1;
            Size = 50;
            SortBy = "Id";
            SortDirection = "asc";
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}