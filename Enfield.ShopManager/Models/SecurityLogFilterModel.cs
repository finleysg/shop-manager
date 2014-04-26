using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class SecurityLogFilterModel
    {
        public SecurityLogFilterModel()
        {
            Page = 1;
            Size = 50;
            SortBy = "LoginDate";
            SortDirection = "asc";
            LoginDateStart = DateTime.Today.AddMonths(-1);
            LoginDateEnd = DateTime.Today.AddDays(1);
            ResultFlag = "All";
            LocationId = -1; //All
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }

        [Display(Name = "Result")]
        public string ResultFlag { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? LoginDateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? LoginDateEnd { get; set; }
    }
}