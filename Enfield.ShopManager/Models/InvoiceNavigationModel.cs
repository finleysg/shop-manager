using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class InvoiceNavigationModel
    {
        public int Id { get; set; }
        public string StockNumber { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
    }
}