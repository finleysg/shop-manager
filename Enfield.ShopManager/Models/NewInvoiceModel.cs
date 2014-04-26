using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class NewInvoiceModel
    {
        public int AccountId { get; set; }
        public int AccountTypeId { get; set; }

        [Required]
        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "Stock Number")]
        public string StockNumber { get; set; }

        [Required]
        [StringLength(4, MinimumLength=4, ErrorMessage="The year must be exactly 4 digits")]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Make")]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}