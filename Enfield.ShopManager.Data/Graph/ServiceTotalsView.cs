using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Graph
{
    public class ServiceTotalsView : AutoMapBase<ServiceTotalsView>
    {
        public virtual string AccountName { get; set; }
        public virtual DateTime CompleteDate { get; set; }
        public virtual int LocationId { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int Cars { get; set; }
    }
}
