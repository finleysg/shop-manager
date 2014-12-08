using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class DealerTotalModel
    {
        public string LocationName { get; set; }
        public string AccountName { get; set; }
        public List<ServiceTotalViewModel> ServicesByDate { get; set; }

        public int Count
        {
            get { return ServicesByDate.Sum(i => i.Cars); }
        }

        public decimal Total
        {
            get { return ServicesByDate.Sum(i => i.Total); }
        }

        public string FormattedCount
        {
            get
            {
                return Count.ToString();
            }
        }

        public string FormattedTotal
        {
            get
            {
                return Total.ToString("C2");
            }
        }
    }
}