using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Models;
using AutoMapper;
using PagedList;
using System.Web.Mvc;

namespace Enfield.ShopManager.Services
{
    public class EmployeeService : DomainServiceBase
    {

        public UserListingModel GetUserListing(UserFilterModel filter)
        {
            if (filter == null) filter = new UserFilterModel();

            var query = Mapper.Map<UserFilterModel, Data.Query.UserQuery>(filter);
            var users = SecurityRepository.GetUsers(query).OrderBy(o => o.Name).ToList();
            var userList = Mapper.Map<List<Data.Graph.Employee>, IList<UserModel>>(users);

            UserListingModel model = new UserListingModel();
            model.Filter = filter;
            model.UserList = userList.ToPagedList(filter.Page, filter.Size);

            return model;
        }

        public SelectList GetActiveEmployees()
        {
            var employees = SecurityRepository.GetUsers(new Data.Query.UserQuery() { HasSiteAccess = true });
            var employeeList = Mapper.Map<IList<Data.Graph.Employee>, IList<EmployeeModel>>(employees);
            return new SelectList(employeeList, "Id", "DisplayName");
        }

        public UserModel GetUser(int id)
        {
            var user = SecurityRepository.GetUser(id);
            return Mapper.Map<Data.Graph.Employee, UserModel>(user);
        }

        public bool IsUsernameUnique(string name)
        {
            var user = SecurityRepository.GetUser(name);
            return (user == null);
        }

        public UserModel UpdateUser(UserModel user)
        {
            var usr = Mapper.Map<UserModel, Data.Graph.Employee>(user);
            var existing = SecurityRepository.GetUser(usr.Id);

            //Allow for changing password
            if (!string.IsNullOrEmpty(user.PasswordString))
            {
                existing.Password = SecurityRepository.GetHashFromPassword(user.PasswordString);
            }
            existing.FirstName = usr.FirstName;
            existing.IsEmployed = usr.IsEmployed;
            existing.LastName = usr.LastName;
            existing.Name = usr.Name;
            existing.Rate = usr.Rate;
            existing.RoleName = usr.RoleName;
            existing.LocationId = usr.LocationId;
            existing.CanLogin = usr.CanLogin;

            usr = SecurityRepository.SaveOrUpdateUser(existing);

            return Mapper.Map<Data.Graph.Employee, UserModel>(usr);
        }

        public UserModel CreateUser(UserModel user)
        {
            var usr = Mapper.Map<UserModel, Data.Graph.Employee>(user);
            usr.Password = SecurityRepository.GetHashFromPassword(user.PasswordString);
            usr = SecurityRepository.SaveOrUpdateUser(usr);

            return Mapper.Map<Data.Graph.Employee, UserModel>(usr);
        }
    }
}