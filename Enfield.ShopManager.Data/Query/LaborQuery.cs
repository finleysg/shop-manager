using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Query
{
    public class LaborQuery : QueryBase
    {
        public LaborQuery()
        {
            EmployeeIds = new List<int>();
        }

        public List<int> EmployeeIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? LocationId { get; set; }
    }
}
