using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Graph
{
    public class AvailableLaborView : AutoMapBase<AvailableLaborView>
    {
        public virtual int AccountTypeServiceId { get; set; }
        public virtual int ServiceTypeId { get; set; }
        public virtual string ServiceTypeDescription { get; set; }
        public virtual int LaborTypeId { get; set; }
        public virtual string LaborTypeDescription { get; set; }
    }
}
