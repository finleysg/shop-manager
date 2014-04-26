using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using Enfield.ShopManager.Services;
using Enfield.ShopManager.Plumbing;
using NUnit.Framework;

namespace Enfield.ShopManager.Tests.Installer
{
    public class DomainServiceInstallerTests
    {
        private IWindsorContainer containerWithDomainServices;

        [SetUp]
        public void DomainServiceInstallerSetup()
        {
            containerWithDomainServices = new WindsorContainer().Install(new DomainServiceInstaller());
        }

        [TearDown]
        public void DomainServiceInstallerTeardown()
        {
            containerWithDomainServices.Dispose();
        }

        [Test]
        public void All_DomainServices_implement_IDomainService()
        {
            var allHandlers = InstallerTestHelper.GetAllHandlers(containerWithDomainServices);
            var DomainServiceHandlers = InstallerTestHelper.GetHandlersFor(typeof(IDomainService), containerWithDomainServices);

            Assert.IsNotEmpty(allHandlers);
            Assert.AreEqual(allHandlers, DomainServiceHandlers);
        }

        //[Test]
        //public void All_and_only_DomainServices_have_Service_suffix()
        //{
        //    var allDomainServices = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Service"));
        //    var registeredDomainServices = InstallerTestHelper.GetImplementationTypesFor(typeof(IDomainService), containerWithDomainServices);
        //    Assert.AreEqual(allDomainServices, registeredDomainServices);
        //}

        [Test]
        public void All_DomainServices_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allDomainServices = GetPublicClassesFromApplicationAssembly(c => c.Is<IDomainService>() && !c.IsAbstract);
            var registeredDomainServices = InstallerTestHelper.GetImplementationTypesFor(typeof(IDomainService), containerWithDomainServices);
            Assert.AreEqual(allDomainServices, registeredDomainServices);
        }

        [Test]
        public void All_DomainServices_are_transient()
        {
            var nonTransientDomainServices = InstallerTestHelper.GetHandlersFor(typeof(IDomainService), containerWithDomainServices)
                .Where(DomainService => DomainService.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.IsEmpty(nonTransientDomainServices);
        }

        //[Test]
        //public void All_DomainServices_expose_themselves_as_service()
        //{
        //    var DomainServicesWithWrongName = GetHandlersFor(typeof(IDomainService), containerWithDomainServices)
        //        .Where(DomainService => DomainService.Service != DomainService.ComponentModel.Implementation)
        //        .ToArray();

        //    Assert.IsEmpty(DomainServicesWithWrongName);
        //}

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(DomainServiceBase).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }

    }
}
