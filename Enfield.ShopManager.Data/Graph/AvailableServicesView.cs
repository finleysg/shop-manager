using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Graph
{
    public class AvailableServicesView : AutoMapBase<AvailableServicesView>
    {
        public virtual int AccountTypeId { get; set; }
        public virtual string AccountTypeDescription { get; set; }
        public virtual int ServiceTypeId { get; set; }
        public virtual string ServiceTypeDescription { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
