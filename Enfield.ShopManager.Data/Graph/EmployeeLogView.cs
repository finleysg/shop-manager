using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class EmployeeLogView : AutoMapBase<EmployeeLogView>
    {
        public virtual int EmployeeId { get; set; }
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual DateTime SignInDate { get; set; }
        public virtual DateTime? SignOutDate { get; set; }
        public virtual Location Location { get; set; }
    }
}
