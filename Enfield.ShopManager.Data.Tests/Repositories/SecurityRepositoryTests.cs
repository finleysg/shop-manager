using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NUnit.Framework;
using Enfield.ShopManager.Data.Repository;
using Enfield.ShopManager.Data.Tests.Fixtures;
using Enfield.ShopManager.Data.Query;

namespace Enfield.ShopManager.Data.Tests.Repositories
{
    public class SecurityRepositoryTests
    {
        ISession session;
        ITransaction tx;
        SecurityRepository repository;

        [SetUp]
        public void Setup()
        {
            session = WindsorPersistenceFixture.Container.Resolve<ISession>();
            repository = new SecurityRepository();
            repository.Session = session;
            tx = session.BeginTransaction();
        }

        [TearDown]
        public void Cleanup()
        {
            tx.Rollback();
            tx.Dispose();
            session.Close();
        }

        //[Test]
        //public void GetUser_ReturnsMe()
        //{
        //    var user = repository.GetUser("stuart");
        //    Assert.IsNotNull(user);
        //    Assert.AreEqual(new Guid("3BB61BDE-F274-4EDC-80C2-AB2DB50AD890"), user.UserId);
        //}

        [Test]
        public void GetUsers_ReturnsAll()
        {
            var users = repository.GetUsers();
            Assert.IsNotNull(users);
            Assert.AreEqual(102, users.Count);
        }

        [Test]
        public void GetUsers_ReturnsAdministrators()
        {
            var query = new UserQuery() { RoleName = "Administrator" };
            var users = repository.GetUsers(query);
            Assert.IsNotNull(users);
            Assert.AreEqual(3, users.Count);
        }

        [Test]
        public void GetUsers_Default_ReturnsEmployeesWithSiteAccess()
        {
            var query = new UserQuery() { RoleName = "Employee", HasSiteAccess = true };
            var users = repository.GetUsers(query);
            Assert.IsNotNull(users);
            Assert.AreEqual(11, users.Count);
        }

        [Test]
        public void GetUsers_ReturnsUsersWithSiteAccess()
        {
            var users = repository.GetUsers(new UserQuery() { HasSiteAccess = true });
            Assert.IsNotNull(users);
            Assert.AreEqual(34, users.Count);
        }
    }
}
