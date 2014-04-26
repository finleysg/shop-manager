using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class DealerStatementsReport
    {
        public List<DealerStatementModel> Statements { get; set; }
        public ReportHeaderModel Header { get; set; }
        public string EndDate { get; set; }
    }
}