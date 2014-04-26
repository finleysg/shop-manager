using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class ServiceTotalViewModel
    {
        public DateTime CompleteDate { get; set; }
        public decimal Total { get; set; }
        public int Cars { get; set; }

        public string FormattedCompleteDate
        {
            get
            {
                return CompleteDate.ToShortDateString();
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