using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enfield.ShopManager.Tests.Helper;
using Enfield.ShopManager.Services;

namespace Enfield.ShopManager.Tests.Services
{
    public class RoleServiceTests
    {
        private IRoleService service;

        [SetUp]
        public void InvoiceServiceSetup()
        {
            service = WindsorPersistenceFixture.Container.Resolve<IRoleService>();
        }

        [TearDown]
        public void InvoiceServiceTeardown()
        {
            service = null;
        }

        [Test]
        public void GetRoles_ReturnsFour()
        {
            var roles = service.GetRoles();
            Assert.IsNotNull(roles);
            Assert.AreEqual(4, roles.Count());
        }

        [Test]
        public void GetRole_ByUser_ReturnsOne()
        {
            var role = service.GetRole("STUART");
            Assert.IsNotNull(role);
            Assert.AreEqual("Administrator", role);
        }

        [Test]
        public void GetRoles_BogusUser_ReturnsNull()
        {
            var role = service.GetRole("BOGUS");
            Assert.IsNull(role);
        }
    }
}
