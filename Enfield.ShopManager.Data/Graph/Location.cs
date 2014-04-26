namespace Enfield.ShopManager.Data.Graph
{
    public class Location : AutoMapBase<Location>
    {
        public Location() {}

        public Location(int id)
        {
            Id = id;
        }

        private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = (value == null) ? null : value.ToUpper(); }
        }

        public virtual string StaticIpAddress { get; set; }
        public virtual int DefaultAccountId { get; set; }
    }
}
