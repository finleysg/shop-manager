using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Graph
{
    public class EmployeeView : AutoMapBase<EmployeeView>
    {
        public virtual string Name { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual bool IsEmployed { get; set; }
        public virtual string RoleName { get; set; }
        public virtual bool CanLogin { get; set; }
    }
}
