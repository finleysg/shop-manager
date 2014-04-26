using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enfield.ShopManager.Models
{
    public class InvoiceViewModel
    {
        public int RowId { get; set; }

        [Display(Name = "Invoice #")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Received")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReceiveDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Completed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CompleteDate { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Stock Number")]
        public string StockNumber { get; set; }

        [Display(Name = "Year")]
        public string Year { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Purchase Order")]
        public string PurchaseOrderNumber { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Tax")]
        public decimal Tax { get; set; }

        public string FormattedVehicleDescription
        {
            get
            {
                return string.Format("{0} {1} {2} {3}", Year, Color, Make, Model);
            }
        }

        public string FormattedTotal
        {
            get
            {
                return Total.ToString("C2");
            }
        }

        public string FormattedTax
        {
            get
            {
                return Tax.ToString("C2");
            }
        }

        public string FormattedGrandTotal
        {
            get
            {
                return (Total+Tax).ToString("C2");
            }
        }
    }
}