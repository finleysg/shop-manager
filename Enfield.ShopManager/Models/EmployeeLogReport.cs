using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class EmployeeLogReport
    {
        public List<EmployeeLogModel> Employees { get; set; }
        public ReportHeaderModel Header { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}