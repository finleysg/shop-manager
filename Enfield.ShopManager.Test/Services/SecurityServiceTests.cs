using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enfield.ShopManager.Tests.Helper;
using Enfield.ShopManager.Services;
using Castle.Facilities.Logging;

namespace Enfield.ShopManager.Tests.Services
{
    public class SecurityServiceTests
    {
        private SecurityService service;

        [SetUp]
        public void InvoiceServiceSetup()
        {
            service = WindsorPersistenceFixture.Container.Resolve<SecurityService>();
        }

        [TearDown]
        public void InvoiceServiceTeardown()
        {
            service = null;
        }

        [Test]
        public void GetUserListing_Default_ReturnsAllUsers()
        {
            var users = service.GetUserListing(null);
            Assert.IsNotNull(users);
            Assert.AreEqual(14, users.UserList.TotalItemCount);
        }
    }
}
