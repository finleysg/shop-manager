using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class LocationModel
    {
        const string ValidIpAddressRegex = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";

        public LocationModel()
        {
            Name = string.Empty;
        }

        public int Id { get; set; }
        public int DefaultAccountId { get; set; }

        [Required]
        [Display(Name = "Location Name")]
        [Remote("ValidateLocationName", "Administration", AdditionalFields = "OriginalName", HttpMethod = "Post", ErrorMessage = "That location name is already in use.")]
        public string Name { get; set; }

        [Display(Name = "IP Address Enforced")]
        [RegularExpression(ValidIpAddressRegex, ErrorMessage = "Enter an IP in the form XXX.XXX.XXX.XXX")]
        public string StaticIpAddress { get; set; }

        [Display(Name = "Default Account Name")]
        public string DefaultAccountName { get; set; }
    }
}