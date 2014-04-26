namespace Enfield.ShopManager.Data.Graph
{
    public class Contact : AutoMapBase<Contact>
    {
        public virtual ContactType ContactType { get; set; }
        public virtual Account Account { get; set; }

        private string lastName;
        public virtual string LastName
        {
            get { return lastName; }
            set { lastName = (value == null) ? null : value.ToUpper(); }
        }

        private string firstName;
        public virtual string FirstName
        {
            get { return firstName; }
            set { firstName = (value == null) ? null : value.ToUpper(); }
        }

        private string contactDetail;
        public virtual string ContactDetail
        {
            get { return contactDetail; }
            set { contactDetail = (value == null) ? null : value.ToUpper(); }
        }

        public virtual bool DoNotify { get; set; }
    }
}
