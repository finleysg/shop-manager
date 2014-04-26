namespace Enfield.ShopManager.Data.Graph
{
    public class ApprovedIp : AutoMapBase
    {
        public virtual int ApprovedIpId { get; set; }
        public virtual int LocationId { get; set; }
        public virtual string RoleName { get; set; }
        public virtual string IpAddress { get; set; }
    }
}
