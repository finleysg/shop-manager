using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Query
{
    public class AccountQuery : QueryBase
    {
        public string AccountName { get; set; }
        public int? AccountTypeId { get; set; }
    }
}
