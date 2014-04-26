using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class ContactModel
    {
        public ContactModel()
        {
            ContactTypeId = 1;
            ContactDetail = "Enter contact detail";
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string ContactTypeDescription { get; set; }

        [Display(Name = "Contact Type")]
        public int ContactTypeId { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Contact Detail")]
        public string ContactDetail { get; set; }
    }
}