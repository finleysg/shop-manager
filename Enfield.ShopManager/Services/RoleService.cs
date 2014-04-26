using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Enfield.ShopManager.Services
{
    public interface IRoleService
    {
        string GetRole(string username);
        IEnumerable<string> GetRoles();
        void AddUserToRole(string username, string role);
        void RemoveUserFromRole(string username, string role);
    }

    public class RoleService : IRoleService
    {
        private RoleProvider _provider;

        public RoleService()
            : this(null)
        {
        }

        public RoleService(RoleProvider provider)
        {
            _provider = provider ?? Roles.Provider;
        }

        public string GetRole(string username)
        {
            var roles = _provider.GetRolesForUser(username);
            if (roles != null && roles.Count() > 0) return roles[0];
            return null;
        }

        public IEnumerable<string> GetRoles()
        {
            return (IEnumerable<string>)_provider.GetAllRoles();
        }

        public void AddUserToRole(string username, string role)
        {
            var roles = _provider.GetAllRoles();
            _provider.RemoveUsersFromRoles(new string[] { username }, roles);
            _provider.AddUsersToRoles(new string[] { username }, new string[] { role });
        }

        public void RemoveUserFromRole(string username, string role)
        {
            _provider.RemoveUsersFromRoles(new string[] { username }, new string[] { role });
        }
    }
}