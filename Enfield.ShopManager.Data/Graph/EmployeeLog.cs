using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Graph
{
    public class EmployeeLog : AutoMapBase<EmployeeLog>
    {
        public EmployeeLog()
        {
            locationId = 1;
        }

        public virtual Employee Employee { get; set; }
        public virtual DateTime SignInDate { get; set; }
        public virtual DateTime? SignOutDate { get; set; }

        private int? locationId;
        public virtual int? LocationId
        {
            get { return (locationId.HasValue) ? locationId.Value : 1; }
            set { locationId = value; }
        }
    }
}
