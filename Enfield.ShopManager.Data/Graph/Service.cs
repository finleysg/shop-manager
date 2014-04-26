using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class Service : AutoMapBase<Service>, Audit.IHaveAuditInformation
    {
        public virtual Invoice Invoice { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual DateTime ServiceDate { get; set; }
        public virtual int EstimatedTime { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }
    }
}
