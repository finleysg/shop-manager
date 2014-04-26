using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class EmployeeLogFilterModel
    {
        public EmployeeLogFilterModel()
        {
            Page = 1;
            Size = 50;
            SortBy = "SignInDate";
            SortDirection = "asc";
            SignInDateStart = DateTime.Today;
            SignInDateEnd = DateTime.Today.AddDays(1);
            LocationId = -1; //All
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SignInDateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SignInDateEnd { get; set; }
    }
}