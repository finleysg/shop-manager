using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Security
{
    public class Token
    {
        public string IpAddress { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Hash { get; set; }

        private RolesEnum role = RolesEnum.Employee;
        public int Role 
        {
            get
            {
                return (int)role;
            }
            set
            {
                role = (RolesEnum)value;
            }
        }

        public string RoleName
        {
            get { return role.ToString(); }
        }

        public bool IsInRole(RolesEnum role)
        {
            return (this.role == role);
        }
    }
}