using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class InvoiceModel
    {
        public InvoiceModel()
        {
            LaborList = new List<LaborModel>();
            ServiceList = new List<ServiceModel>();
            History = new List<HistoryModel>();
        }

        [Display(Name = "Invoice #")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Received")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReceiveDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Completed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CompleteDate { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [Required]
        [Display(Name = "Stock Number")]
        public string StockNumber { get; set; }

        [Display(Name = "VIN")]
        public string VIN { get; set; }

        [Required]
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

        [Required]
        [Display(Name = "Purchase Order")]
        public string PurchaseOrderNumber { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Completed")]
        public bool IsComplete { get; set; }

        public decimal TaxRate { get; set; }

        public string CompleteDateTime
        {
            get 
            {
                if (CompleteDate.HasValue) return CompleteDate.Value.ToShortTimeString();
                else return string.Empty;
            }
        }

        public string FormattedVehicle
        {
            get { return string.Format("{0} {1} {2} {3}", Year, Color, Make, Model); }
        }

        public List<LaborModel> LaborList { get; set; }
        public List<ServiceModel> ServiceList { get; set; }
        public List<HistoryModel> History { get; set; }
    }
}