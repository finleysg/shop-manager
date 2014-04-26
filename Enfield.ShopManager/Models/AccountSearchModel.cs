using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class AccountSearchModel
    {
        public int Id;
        public string Name;
        public string AccountNumber { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountTypeDescription { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0} ({1})", Name, AccountNumber); }
        }
    }
}