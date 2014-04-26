using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class PayrollStatementModel
    {
        public EmployeeModel Employee { get; set; }
        public List<PayrollModel> Labor { get; set; }

        public string FormattedCount
        {
            get { return Labor.Count.ToString(); }
        }

        public string FormattedTotal
        {
            get { return Labor.Sum(i => i.ActualRate).ToString("C2"); }
        }
    }
}