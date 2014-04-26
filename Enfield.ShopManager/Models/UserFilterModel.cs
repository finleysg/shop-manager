using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class UserFilterModel : PagingFilterModel
    {
        public UserFilterModel()
            : base()
        {
            HasSiteAccess = "True";
            Role = "Employee";
        }

        [Display(Name = "Can Sign In?")]
        public string HasSiteAccess { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name="User Name")]
        public string Name { get; set; }
    }
}