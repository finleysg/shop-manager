using System;

namespace Enfield.ShopManager.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string ServiceTypeDescription { get; set; }
        public decimal Rate { get; set; }
    }
}