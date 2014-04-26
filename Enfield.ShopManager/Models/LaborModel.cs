using System;

namespace Enfield.ShopManager.Models
{
    public class LaborModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string LaborTypeDescription { get; set; }
        public decimal ActualRate { get; set; }
        public DateTime LaborDate { get; set; }
        public int InvoiceId { get; set; }
    }
}