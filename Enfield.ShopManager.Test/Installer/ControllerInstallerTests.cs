using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using Enfield.ShopManager.Controllers;
using Enfield.ShopManager.Plumbing;
using NUnit.Framework;

namespace Enfield.ShopManager.Tests.Installer
{
    public class ControllerInstallerTests
    {
        private IWindsorContainer containerWithControllers;
 
        [SetUp]
        public void ControllerInstallerSetup()
        {
            containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());
        }

        [TearDown]
        public void ControllerInstallerTeardown()
        {
            containerWithControllers.Dispose();
        }

        [Test]
        public void All_controllers_implement_IController()
        {
            var allHandlers = InstallerTestHelper.GetAllHandlers(containerWithControllers);
            var controllerHandlers = InstallerTestHelper.GetHandlersFor(typeof(IController), containerWithControllers);

            Assert.IsNotEmpty(allHandlers);
            Assert.AreEqual(allHandlers, controllerHandlers);
        }

        [Test]
        public void All_and_only_controllers_have_Controllers_suffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = InstallerTestHelper.GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void All_and_only_controllers_live_in_Controllers_namespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = InstallerTestHelper.GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void All_controllers_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = InstallerTestHelper.GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void All_controllers_are_transient()
        {
            var nonTransientControllers = InstallerTestHelper.GetHandlersFor(typeof(IController), containerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.IsEmpty(nonTransientControllers);
        }

        //[Test]
        //public void All_controllers_expose_themselves_as_service()
        //{
        //    var controllersWithWrongName = GetHandlersFor(typeof(IController), containerWithControllers)
        //        .Where(controller => controller.Service != controller.ComponentModel.Implementation)
        //        .ToArray();

        //    Assert.IsEmpty(controllersWithWrongName);
        //}

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(ShopFloorController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
    }
}
