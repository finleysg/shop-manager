using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Enfield.ShopManager.Services;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Rate = "1.0";
            RoleName = "Employee";
            IsEmployed = true;
            StartDate = DateTime.Today;
        }

        public int Id { get; set; }
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "Login Name")]
        [Remote("ValidateUserName", "Administration", AdditionalFields="OriginalName", HttpMethod="Post", ErrorMessage = "That login name is already taken.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,3})?$", ErrorMessage = "Enter a valid numeric rate.")]
        [Display(Name = "Rate")]
        public string Rate { get; set; }

        [Display(Name = "Password")]
        public string PasswordString { get; set; }

        [Display(Name = "Can Sign In?")]
        public bool IsEmployed { get; set; }

        [Display(Name = "Can Log In?")]
        public bool CanLogin { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
}