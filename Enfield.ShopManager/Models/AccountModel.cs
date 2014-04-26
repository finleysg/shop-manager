using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
            ContactList = new List<ContactModel>();
            StateCode = "TN";
        }

        public int Id { get; set; }
        public int AccountTypeId { get; set; }

        [Display(Name = "Account Type")]
        public string AccountTypeDescription { get; set; }

        [Display(Name = "Account Name")]
        public string Name { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string StateCode { get; set; }

        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public List<ContactModel> ContactList { get; set; }

        public string FormattedAddressLine1
        {
            get
            {
                return string.Format("{0} {1}", AddressLine1, AddressLine2).Trim();
            }
        }

        public string FormattedAddressLine2
        {
            get
            {
                if (string.IsNullOrEmpty(City) &&
                    string.IsNullOrEmpty(StateCode) &&
                    string.IsNullOrEmpty(PostalCode)) return "";

                return string.Format("{0}, {1} {2}", City, StateCode, PostalCode).Trim();
            }
        }
    }
}