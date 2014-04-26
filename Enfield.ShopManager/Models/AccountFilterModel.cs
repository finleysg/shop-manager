using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class AccountFilterModel : PagingFilterModel
    {
        public AccountFilterModel()
        {
            AccountTypeId = 1;
        }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Account Type")]
        public int? AccountTypeId { get; set; }
    }
}