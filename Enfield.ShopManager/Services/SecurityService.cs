using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enfield.ShopManager.Data.Graph;
using Enfield.ShopManager.Models;
using PagedList;
using System.Web;

namespace Enfield.ShopManager.Services
{
    public class SecurityService : DomainServiceBase
    {
        public UserModel ValidateUser(int userId, string password)
        {
            var user = SecurityRepository.GetUser(userId);
            if (user == null)
            {
                Logger.WarnFormat("user id {0} was not found", userId);
                return null;
            }

            var pwdHash = SecurityRepository.GetHashFromPassword(password);

            if (user.Password == null) return null;
            if (!ValidatePassword(pwdHash, user.Password)) return null;

            return Mapper.Map<Data.Graph.Employee, UserModel>(user);
        }

        public UserModel ValidateUser(string userName, string password)
        {
            var user = SecurityRepository.GetUser(userName);
            if (user == null)
            {
                Logger.WarnFormat("user {0} was not found", userName);
                return null;
            }

            var pwdHash = SecurityRepository.GetHashFromPassword(password);

            if (user.Password == null) return null;
            if (!ValidatePassword(pwdHash, user.Password)) return null;

            return Mapper.Map<Data.Graph.Employee, UserModel>(user);
        }

        public void RecordSuccessfulLoginAttempt(LoginModel login, string ipAddress, string message = null)
        {
            LoginAttemptLog log = new LoginAttemptLog()
            {
                IpAddress = ipAddress,
                Location = new Location(login.LocationId),
                LoginDate = DateTime.Now,
                Reason = string.IsNullOrEmpty(message) ? null : message,
                ResultFlag = true,
                UserName = login.Name
            };
            SecurityRepository.InsertLoginAttempt(log);
        }

        public void RecordFailedLoginAttempt(LoginModel login, string ipAddress, string reason)
        {
            LoginAttemptLog log = new LoginAttemptLog()
            {
                IpAddress = ipAddress,
                Location = new Location(login.LocationId),
                LoginDate = DateTime.Now,
                Reason = reason,
                ResultFlag = false,
                UserName = login.Name
            };
            SecurityRepository.InsertLoginAttempt(log);
        }

        public void SignIn(UserModel employee, int locationId)
        {
            //Don't allow multiple sign-ins per day/location
            var log = GetEmployeeLogFromCache(locationId, false);
            if (null == log.Where(l => l.Employee.Id == employee.Id).FirstOrDefault())
            {
                var graph = Mapper.Map<UserModel, Data.Graph.Employee>(employee);
                SecurityRepository.InsertEmployeeLog(graph, locationId);
            }
        }

        public void SignOut(UserModel employee, int locationId)
        {
            var log = GetEmployeeLogFromCache(locationId, false);
            if (null != log.Where(l => l.Employee.Id == employee.Id).FirstOrDefault())
            {
                var graph = Mapper.Map<UserModel, Data.Graph.Employee>(employee);
                SecurityRepository.UpdateEmployeeLog(graph, locationId);
            }
        }

        public void SignOut(EmployeeModel employee, int locationId)
        {
            var log = GetEmployeeLogFromCache(locationId, false);
            if (null != log.Where(l => l.Employee.Id == employee.Id).FirstOrDefault())
            {
                var graph = Mapper.Map<EmployeeModel, Data.Graph.Employee>(employee);
                SecurityRepository.UpdateEmployeeLog(graph, locationId);
            }
        }

        public SecurityLogListingModel GetSecurityLog(SecurityLogFilterModel filter)
        {
            if (filter == null) filter = new SecurityLogFilterModel();

            var query = Mapper.Map<SecurityLogFilterModel, Data.Query.LoginQuery>(filter);
            var log = SecurityRepository.GetSecurityLog(query).OrderByDescending(o => o.LoginAttemptId).ToList();
            var security = Mapper.Map<IList<Data.Graph.LoginAttemptLog>, IList<SecurityLogModel>>(log);

            SecurityLogListingModel model = new SecurityLogListingModel();
            model.Filter = filter;
            model.SecurityLog = security.ToPagedList(filter.Page, filter.Size);

            return model;
        }

    }
}