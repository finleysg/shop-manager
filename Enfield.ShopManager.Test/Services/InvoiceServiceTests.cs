using System;
using Castle.Windsor;
using Enfield.ShopManager.Mapping;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Plumbing;
using Enfield.ShopManager.Services;
using NUnit.Framework;
using Enfield.ShopManager.Tests.Helper;

namespace Enfield.ShopManager.Tests.Services
{
    public class InvoiceServiceTests
    {
        private InvoiceFilterModel filter;
        private InvoiceAdministrationService service;

        [SetUp]
        public void InvoiceServiceSetup()
        {
            service = WindsorPersistenceFixture.Container.Resolve<InvoiceAdministrationService>();

            filter = new InvoiceFilterModel();
            filter.AccountName = "DOBBS FORD AT MT. MORIAH";
            filter.ReceivedDateStart = DateTime.Parse("12/7/2011");
            filter.ReceivedDateEnd = DateTime.Parse("12/10/2011");
        }

        [TearDown]
        public void InvoiceServiceTeardown()
        {
            service = null;
        }

        [Test]
        public void GetInvoiceListing_NullFilter_CorrectDefaults()
        {
            var model = service.GetInvoiceListing(null);
            Assert.AreEqual(-1, model.Filter.LocationId.Value);
            Assert.AreEqual("Not Paid", model.Filter.HasBeenPaid);
            Assert.AreEqual(1, model.Filter.Page);
            Assert.AreEqual(50, model.Filter.Size);
        }

        [Test]
        public void GetInvoiceListing_ReturnsAllFour()
        {
            filter.Page = 1;
            filter.Size = 4;
            var model = service.GetInvoiceListing(filter);
            Assert.IsNotNull(model);
            Assert.AreEqual(4, model.InvoiceList.Count);
        }

        [Test]
        public void GetInvoiceListing_ReturnsPageOne()
        {
            filter.Page = 1;
            filter.Size = 2;
            var model = service.GetInvoiceListing(filter);
            Assert.IsNotNull(model);
            Assert.AreEqual(4, model.InvoiceList.TotalItemCount);
            Assert.AreEqual(2, model.InvoiceList.Count);
            Assert.AreEqual(2, model.InvoiceList.PageCount);
            Assert.AreEqual(2, model.InvoiceList.PageSize);
            Assert.AreEqual(1, model.InvoiceList.PageNumber);
        }

        //[Test]
        //public void GetInvoiceAdministrationDetail_NullFilter_CorrectDefaults()
        //{
        //    var model = service.GetInvoiceAdministrationDetail(null);
        //    Assert.AreEqual(-1, model.Filter.LocationId.Value);
        //    Assert.IsFalse(model.Filter.HasBeenPaid.Value);
        //    Assert.AreEqual(1, model.Filter.Page);
        //    Assert.AreEqual(1, model.Filter.Size);
        //}

        [Test]
        public void GetInvoiceAdministrationDetail_BogusFilter_EmptyModel()
        {
            var model = service.GetInvoiceAdministrationDetail(new InvoiceFilterModel() { InvoiceId = -1 });
            Assert.IsNotNull(model);
            Assert.AreEqual(-1, model.Filter.InvoiceId);
        }

        [Test]
        public void GetInvoiceAdministrationDetail_BadPage_ReturnsFirst()
        {
            filter.Page = 6;
            var model = service.GetInvoiceAdministrationDetail(filter);
            Assert.AreEqual(358600, model.CurrentInvoice.Id);
            Assert.AreEqual(1, model.Filter.Page);
        }

        [Test]
        public void GetInvoiceAdministrationDetail_ReturnsFirst()
        {
            var model = service.GetInvoiceAdministrationDetail(filter);
            Assert.AreEqual(4, model.InvoiceList.TotalItemCount);
            Assert.IsNotNull(model.CurrentInvoice);
            Assert.AreEqual(358600, model.CurrentInvoice.Id);
            Assert.AreEqual(1, model.CurrentInvoice.ServiceList.Count);
            Assert.AreEqual(1, model.CurrentInvoice.LaborList.Count);
            Assert.AreEqual("FLEET DETAIL", model.CurrentInvoice.ServiceList[0].ServiceTypeDescription);
        }
    }
}
