using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class Employee : AutoMapBase<Employee>, Audit.IHaveAuditInformation
    {
        public Employee()
        {
            //no-op
        }

        public Employee(int id)
        {
            base.Id = id;
        }

        public virtual int LocationId { get; set; }

        private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = (value == null) ? null : value.ToUpper(); }
        }

        private string firstname;
        public virtual string FirstName
        {
            get { return firstname; }
            set { firstname = (value == null) ? null : value.ToUpper(); }
        }

        private string lastname;
        public virtual string LastName
        {
            get { return lastname; }
            set { lastname = (value == null) ? null : value.ToUpper(); }
        }

        public virtual DateTime? StartDate { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual bool IsEmployed { get; set; }
        public virtual string RoleName { get; set; }
        public virtual byte[] Password { get; set; }
        public virtual bool CanLogin { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }
    }
}
