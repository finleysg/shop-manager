using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enfield.ShopManager.Controllers;
using Castle.Windsor;
using Enfield.ShopManager.Plumbing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Enfield.ShopManager.Models;
using MvcContrib.TestHelper;
using Enfield.ShopManager.Services;
using Rhino.Mocks;
using Enfield.ShopManager.Mapping;

namespace Enfield.ShopManager.Tests.Controllers
{
    public class AdministrationControllerTests
    {
        private AdministrationController controller;

        [SetUp]
        public void ControllerInstallerSetup()
        {
            MapConfiguration.Bootstrap();

            var invoiceService = MockRepository.GenerateStub<InvoiceService>();
            invoiceService.Stub(a => a.GetInvoiceListing(null))
                .IgnoreArguments()
                .Return(new InvoiceListingModel());

            controller = new AdministrationController();
            controller.InvoiceServices = invoiceService;

        }

        [TearDown]
        public void ControllerInstallerTeardown()
        {
            //container.Dispose();
        }

        [Test]
        [Ignore("broken")]
        public void InvoiceListing_ReturnsCorrectView()
        {
            var result = controller.InvoiceListing(null);
            result.AssertViewRendered().ForView("InvoiceListing");
        }

        [Test]
        [Ignore("broken")]
        public void InvoiceListing_ReturnsCorrectModel()
        {
            var result = controller.InvoiceListing(null);
            result.AssertViewRendered().WithViewData<InvoiceListingModel>();
        }
    }
}
