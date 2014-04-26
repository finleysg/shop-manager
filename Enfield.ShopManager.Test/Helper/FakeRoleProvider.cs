using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Enfield.ShopManager.Tests.Helper
{
    public class FakeRoleProvider : System.Web.Security.WindowsTokenRoleProvider
    {
        public override string[] GetAllRoles()
        {
            return new string[] {"Employee","Dealer","Manager","Administrator"};
        }

        public override string[] GetRolesForUser(string username)
        {
            if (username.Equals("STUART", StringComparison.InvariantCultureIgnoreCase))
                return new string[] { "Administrator" };
            return new string[] { };
        }
    }
}
