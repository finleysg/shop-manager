using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Rate { get; set; }
        public bool IsEmployed { get; set; }
        public string RoleName { get; set; }
        public bool CanLogin { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
}