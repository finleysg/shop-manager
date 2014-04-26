using System.Collections.Generic;
using Enfield.ShopManager.Data.Graph;
using Enfield.ShopManager.Data.Query;
using NHibernate.Criterion;
using System;
using System.Text;
using System.Security.Cryptography;

namespace Enfield.ShopManager.Data.Repository
{
    public class SecurityRepository : RepositoryBase
    {
        private static Guid salt = new Guid("A136E190-6177-4248-9BB2-ED27633C6ED5");

        public Location GetLocation(int id)
        {
            var location = Session.QueryOver<Location>()
                .Where(l => l.Id == id).SingleOrDefault();
            return location;
        }

        public IList<Location> GetLocations()
        {
            return Session.QueryOver<Location>().List();
        }

        public Location SaveOrUpdateLocation(Location location)
        {
            Session.SaveOrUpdate(location);
            return location;
        }

        public IList<LoginAttemptLog> GetSecurityLog(LoginQuery query)
        {
            var criteria = Session.CreateCriteria<LoginAttemptLog>();

            if (!string.IsNullOrEmpty(query.UserName)) criteria.Add(Expression.Like("UserName", query.UserName + "%"));
            if (query.LocationId.HasValue) criteria.CreateAlias("Location", "l").Add(Expression.Eq("l.Id", query.LocationId.Value));
            if (query.LoginDateStart.HasValue) criteria.Add(Expression.Ge("LoginDate", query.LoginDateStart.Value));
            if (query.LoginDateEnd.HasValue) criteria.Add(Expression.Le("LoginDate", query.LoginDateEnd.Value));
            if (query.ResultFlag.HasValue) criteria.Add(Expression.Eq("ResultFlag", query.ResultFlag.Value));

            if (string.IsNullOrEmpty(query.SortBy))
                criteria.AddOrder(new Order("Id", true));
            else
                criteria.AddOrder(new Order(query.SortBy, (query.SortDirection != null && query.SortDirection.Equals("asc", StringComparison.InvariantCultureIgnoreCase))));

            return criteria.List<LoginAttemptLog>();
        }

        public LoginAttemptLog InsertLoginAttempt(LoginAttemptLog login)
        {
            Session.Save(login);
            return login;
        }

        public IList<EmployeeLog> GetEmployeeLog(SignInQuery query)
        {
            var criteria = Session.CreateCriteria<EmployeeLog>();

            if (!string.IsNullOrEmpty(query.UserName)) criteria.CreateAlias("Employee", "e").Add(Expression.Like("e.Name", query.UserName + "%"));
            if (query.LocationId.HasValue) criteria.Add(Expression.Eq("LocationId", query.LocationId.Value));
            if (query.SignInDateStart.HasValue) criteria.Add(Expression.Ge("SignInDate", query.SignInDateStart.Value));
            if (query.SignInDateEnd.HasValue) criteria.Add(Expression.Le("SignInDate", query.SignInDateEnd.Value));
            criteria.Add(Expression.IsNull("SignOutDate"));

            if (string.IsNullOrEmpty(query.SortBy))
                criteria.AddOrder(new Order("Id", true));
            else
                criteria.AddOrder(new Order(query.SortBy, (query.SortDirection != null && query.SortDirection.Equals("asc", StringComparison.InvariantCultureIgnoreCase))));

            return criteria.List<EmployeeLog>();
        }

        public EmployeeLog InsertEmployeeLog(Employee employee, int locationId)
        {
            EmployeeLog log = new EmployeeLog();
            log.Employee = employee;
            log.SignInDate = DateTime.Now;
            log.LocationId = locationId;

            Session.Save(log);

            return log;
        }

        public EmployeeLog UpdateEmployeeLog(Employee employee, int locationId)
        {
            var log = Session.QueryOver<EmployeeLog>()
                .Where(e => e.Employee.Id == employee.Id)
                .And(l => l.LocationId == locationId)
                .OrderBy(o => o.SignInDate).Desc
                .Take(1).SingleOrDefault();

            if (log != null && 
                log.SignOutDate == null &&
                log.SignInDate.Day == DateTime.Now.Day)
            {
                log.SignOutDate = DateTime.Now;
                Session.Save(log);
            }
            return log;
        }

        public Employee GetUser(int id)
        {
            var user = Session.QueryOver<Employee>()
                .Where(e => e.Id == id).SingleOrDefault();
            return user;
        }

        public Employee GetUser(string name)
        {
            var user = Session.QueryOver<Employee>()
                .Where(e => e.Name == name).SingleOrDefault();
            return user;
        }

        public IList<EmployeeView> GetUsers()
        {
            return Session.QueryOver<EmployeeView>().List();
        }

        public IList<Employee> GetUsers(UserQuery query)
        {
            var criteria = Session.CreateCriteria<Employee>();

            if (query.HasSiteAccess.HasValue) criteria.Add(Expression.Eq("IsEmployed", query.HasSiteAccess.Value));
            if (!string.IsNullOrEmpty(query.Name)) criteria.Add(Expression.Like("Name", string.Format("%{0}%", query.Name)));
            if (!string.IsNullOrEmpty(query.RoleName)) criteria.Add(Expression.Eq("RoleName", query.RoleName));

            if (string.IsNullOrEmpty(query.SortBy))
                criteria.AddOrder(new Order("Name", true));
            else
                criteria.AddOrder(new Order(query.SortBy, (query.SortDirection != null && query.SortDirection.Equals("asc", StringComparison.InvariantCultureIgnoreCase))));

            return criteria.List<Employee>();
        }

        public Employee SaveOrUpdateUser(Employee user)
        {
            user.ModifyDate = DateTime.Now;
            if (System.Web.HttpContext.Current != null &&
                !string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                user.ModifyUser = System.Web.HttpContext.Current.User.Identity.Name;
            else
                user.ModifyUser = "admin";

            Session.SaveOrUpdate(user);
            return user;
        }

        public byte[] GetHashFromPassword(string password)
        {
            //merge salt and password
            var buf = new byte[salt.ToByteArray().Length + Encoding.UTF8.GetByteCount(password)];
            salt.ToByteArray().CopyTo(buf, 0);
            Encoding.UTF8.GetBytes(password).CopyTo(buf, salt.ToByteArray().Length);

            //hash the password
            using (SHA1CryptoServiceProvider crypto = new SHA1CryptoServiceProvider())
            {
                return crypto.ComputeHash(buf);
            }
        }

    }
}
