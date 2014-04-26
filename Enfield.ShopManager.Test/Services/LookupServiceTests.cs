using System.Linq;
using Castle.Windsor;
using Enfield.ShopManager.Plumbing;
using Enfield.ShopManager.Services;
using Enfield.ShopManager.Tests.Helper;
using NUnit.Framework;

namespace Enfield.ShopManager.Tests.Services
{
    public class LookupServiceTests
    {
        private LookupService service;

        [SetUp]
        public void InvoiceServiceSetup()
        {
            service = WindsorPersistenceFixture.Container.Resolve<LookupService>();
        }

        [TearDown]
        public void InvoiceServiceTeardown()
        {
            service = null;
        }

        [Test]
        public void GetLocationLookup_Default_ReturnsAllLocations()
        {
            var lookup = service.GetLocationLookup();
            Assert.IsNotNull(lookup);
            Assert.AreEqual(3, lookup.Count);
            Assert.IsNull(lookup.Where(l => l.Selected).FirstOrDefault());
        }

        [Test]
        public void GetLocationLookup_IncludeAll_ReturnsAllLocationsAndAll()
        {
            var lookup = service.GetLocationLookup(includeAll: true);
            Assert.IsNotNull(lookup);
            Assert.AreEqual(4, lookup.Count);
            Assert.IsNull(lookup.Where(l => l.Selected).FirstOrDefault());
        }

        [Test]
        public void GetLocationLookup_SelectMain_ReturnsAllLocationsWithMainSelected()
        {
            var lookup = service.GetLocationLookup(includeAll: true, selected: 1);
            Assert.IsNotNull(lookup);
            Assert.AreEqual(4, lookup.Count);
            Assert.IsNotNull(lookup.Where(l => l.Selected).FirstOrDefault());
            Assert.AreEqual("1", lookup.Where(l => l.Selected).First().Value);
        }

        [Test]
        public void GetPaidLookup_Default_ReturnsPaidAndNotPaid()
        {
            var lookup = service.GetPaidLookup();
            Assert.IsNotNull(lookup);
            Assert.AreEqual(2, lookup.Count);
            Assert.IsNull(lookup.Where(l => l.Selected).FirstOrDefault());
        }

        [Test]
        public void GetPaidLookup_IncludeAll_ReturnsPaidAndNotPaidAndAll()
        {
            var lookup = service.GetPaidLookup(includeAll: true);
            Assert.IsNotNull(lookup);
            Assert.AreEqual(3, lookup.Count);
            Assert.IsNull(lookup.Where(l => l.Selected).FirstOrDefault());
        }

        [Test]
        public void GetPaidLookup_SelectAll_ReturnsAllSelected()
        {
            var lookup = service.GetPaidLookup(includeAll: true, selected: true);
            Assert.IsNotNull(lookup);
            Assert.AreEqual(3, lookup.Count);
            Assert.IsNotNull(lookup.Where(l => l.Selected).FirstOrDefault());
            Assert.AreEqual("True", lookup.Where(l => l.Selected).First().Value);
        }
    }
}
