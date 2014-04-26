using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class InvoiceFilterModel
    {
        public InvoiceFilterModel()
        {
            Page = 1;
            Size = 50;
            SortBy = "Id";
            SortDirection = "asc";
            //ReceivedDateStart = DateTime.Today.AddMonths(-1);
            ReceivedDateEnd = DateTime.Today.AddDays(1);
            HasBeenPaid = "Not Paid";
            LocationId = -1; //All
            ExcludeZeroTotal = true;
            DoEvaluate = true; //when false, get cached results
        }

        public bool DoEvaluate { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public DateTime? CompletedDateStart { get; set; }
        public DateTime? CompletedDateEnd { get; set; }

        [Display(Name = "Invoice #")]
        public int? InvoiceId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ReceivedDateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ReceivedDateEnd { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Paid?")]
        public string HasBeenPaid { get; set; }

        [Display(Name = "Exclude Zero Totals?")]
        public bool ExcludeZeroTotal { get; set; }

    }
}