using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class Labor : AutoMapBase<Labor>, Audit.IHaveAuditInformation
    {
        public virtual Invoice Invoice { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LaborType LaborType { get; set; }
        public virtual decimal EstimatedRate { get; set; }
        public virtual decimal ActualRate { get; set; }
        public virtual DateTime LaborDate { get; set; }
        public virtual int EstimatedTime { get; set; }
        public virtual int ActualTime { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }
    }
}
