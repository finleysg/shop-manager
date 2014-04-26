using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class ContactDetailModel
    {
        public ContactModel Contact { get; set; }
        public string Action { get; set; }
        public AccountFilterModel Filter { get; set; }
    }
}