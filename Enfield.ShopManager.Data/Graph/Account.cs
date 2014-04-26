using System;
using System.Collections.Generic;

namespace Enfield.ShopManager.Data.Graph
{
    public class Account : AutoMapBase<Account>, Audit.IHaveAuditInformation
    {
        public virtual AccountType AccountType { get; set; }
        public virtual IList<Contact> ContactList { get; set; }
        public virtual string AccountNumber { get; set; }

        private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = (value == null) ? null : value.ToUpper(); }
        }

        private string addressLine1;
        public virtual string AddressLine1
        {
            get { return addressLine1; }
            set { addressLine1 = (value == null) ? null : value.ToUpper(); }
        }

        private string addressLine2;
        public virtual string AddressLine2
        {
            get { return addressLine2; }
            set { addressLine2 = (value == null) ? null : value.ToUpper(); }
        }

        private string city;
        public virtual string City
        {
            get { return city; }
            set { city = (value == null) ? null : value.ToUpper(); }
        }

        private string stateCode;
        public virtual string StateCode
        {
            get { return stateCode; }
            set { stateCode = (value == null) ? null : value.ToUpper(); }
        }

        private string notes;
        public virtual string Notes
        {
            get { return notes; }
            set { notes = (value == null) ? null : value.ToUpper(); }
        }

        public virtual string PostalCode { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }

        public Account()
        {
            ContactList = new List<Contact>();
        }

        public Account(int id)
        {
            ContactList = new List<Contact>();
            Id = id;
        }

        public virtual void AddContact(Contact child)
        {
            child.Account = this;
            ContactList.Add(child);
        }
    }
}
