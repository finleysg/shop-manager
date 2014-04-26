using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Enfield.ShopManager.Services
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        MembershipUser GetUser(Guid userId);
        MembershipUser GetUser(string username);
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        string ResetPassword(string userName);
        void UpdateUser(MembershipUser user);
    }

    public class MembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public MembershipService()
            : this(null)
        {
        }

        public MembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public MembershipUser GetUser(Guid userId)
        {
            return _provider.GetUser(userId, false);
        }

        public MembershipUser GetUser(string username)
        {
            return _provider.GetUser(username, false);
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }

        public void UpdateUser(MembershipUser user)
        {
            _provider.UpdateUser(user);
        }

        public string ResetPassword(string username)
        {
            return _provider.ResetPassword(username, null);
        }
    }
}