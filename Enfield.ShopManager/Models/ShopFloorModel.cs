using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class ShopFloorModel
    {
        public InvoiceModel Invoice { get; set; }
        public ServicesModel Services { get; set; }
        public List<InvoiceNavigationModel> VehiclesInShop { get; set; }
        public List<InvoiceNavigationModel> VehiclesCompleted { get; set; }
        public string NoDataMessage { get; set; }
    }
}