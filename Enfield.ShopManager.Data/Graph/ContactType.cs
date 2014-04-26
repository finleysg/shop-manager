namespace Enfield.ShopManager.Data.Graph
{
    public class ContactType : AutoMapBase<ContactType>
    {
        private string description;
        public virtual string Description
        {
            get { return description; }
            set { description = (value == null) ? null : value.ToUpper(); }
        }
    }
}
