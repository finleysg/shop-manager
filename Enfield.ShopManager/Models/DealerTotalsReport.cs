using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class DealerTotalsReport
    {
        public List<DealerTotalModel> Totals { get; set; }
        public ReportHeaderModel Header { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public int Count
        {
            get { return Totals.Sum(i => i.Count); }
        }

        public decimal Total
        {
            get { return Totals.Sum(i => i.Total); }
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