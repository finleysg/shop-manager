using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class PayrollStatementsReport
    {
        public List<PayrollStatementModel> Statements { get; set; }
        public ReportHeaderModel Header { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}