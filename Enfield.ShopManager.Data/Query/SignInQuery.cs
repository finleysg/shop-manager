using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Query
{
    public class SignInQuery : QueryBase
    {
        public int? LocationId { get; set; }
        public string UserName { get; set; }
        public DateTime? SignInDateStart { get; set; }
        public DateTime? SignInDateEnd { get; set; }
    }
}
