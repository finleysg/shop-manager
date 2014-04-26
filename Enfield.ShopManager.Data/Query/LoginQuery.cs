using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Query
{
    public class LoginQuery : QueryBase
    {
        public bool? ResultFlag { get; set; }
        public int? LocationId { get; set; }
        public string UserName { get; set; }
        public DateTime? LoginDateStart { get; set; }
        public DateTime? LoginDateEnd { get; set; }
    }
}
