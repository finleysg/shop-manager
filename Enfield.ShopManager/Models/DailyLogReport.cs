using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class DailyLogReport
    {
        public List<DailyLogModel> Invoices { get; set; }
        public ReportHeaderModel Header { get; set; }
        public string StartDate { get; set; }
    }
}