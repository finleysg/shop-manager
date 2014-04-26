using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using NUnit.Framework;
using Enfield.ShopManager.Plumbing;
using Enfield.ShopManager.Services;
using Enfield.ShopManager.Tests.Helper;

namespace Enfield.ShopManager.Tests.Services
{
    public class AccountServiceTests
    {
        private AccountService service;

        [SetUp]
        public void InvoiceServiceSetup()
        {
            service = WindsorPersistenceFixture.Container.Resolve<AccountService>();
        }

        [TearDown]
        public void InvoiceServiceTeardown()
        {
            service = null;
        }

        [Test]
        public void SearchAccounts_Default_ReturnsAllAccounts()
        {
            var lookup = service.SearchAccounts(null);
            Assert.IsNotNull(lookup);
            Assert.AreEqual(3, lookup.Count);
        }

        [Test]
        public void SearchAccounts_Dobbs_ReturnsTwoAccounts()
        {
            var lookup = service.SearchAccounts("DOBB");
            Assert.IsNotNull(lookup);
            Assert.AreEqual(2, lookup.Count);
        }

        [Test]
        public void SearchAccounts_CaseInsensitive_ReturnsTwoAccounts()
        {
            var lookup = service.SearchAccounts("dobb");
            Assert.IsNotNull(lookup);
            Assert.AreEqual(2, lookup.Count);
        }

        [Test]
        public void SearchAccounts_IncludesSpace_ReturnsOneAccount()
        {
            var lookup = service.SearchAccounts("BILL G");
            Assert.IsNotNull(lookup);
            Assert.AreEqual(1, lookup.Count);
        }
    }
}
