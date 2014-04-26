using System;
using System.Linq;
using Castle.Core;
using Castle.Core.Internal;
using Castle.Windsor;
using Enfield.ShopManager.Data.Repository;
using Enfield.ShopManager.Plumbing;
using Enfield.ShopManager.Tests.Helper;
using NUnit.Framework;

namespace Enfield.ShopManager.Tests
{
    public class RepositoryInstallerTest
    {
        private IWindsorContainer containerWithRepositories;

        [SetUp]
        public void RepositoryInstallerSetup()
        {
            containerWithRepositories = new WindsorContainer().Install(new RepositoryInstaller());
        }

        [TearDown]
        public void RepositoryInstallerTeardown()
        {
            containerWithRepositories.Dispose();
        }

        //[Test]
        //public void All_Repositories_implement_IRepository()
        //{
        //    var allHandlers = InstallerTestHelper.GetAllHandlers(containerWithRepositories);
        //    var RepositoryHandlers = InstallerTestHelper.GetHandlersFor(typeof(IRepository), containerWithRepositories);

        //    Assert.IsNotEmpty(allHandlers);
        //    Assert.AreEqual(allHandlers, RepositoryHandlers);
        //}

        [Test]
        public void All_and_only_Repositories_have_Repository_suffix()
        {
            var allRepositorys = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Repository"));
            var registeredRepositorys = InstallerTestHelper.GetImplementationTypesFor(typeof(IRepository), containerWithRepositories);
            Assert.AreEqual(allRepositorys, registeredRepositorys);
        }

        //[Test]
        //public void All_and_only_Repositories_live_in_Repository_namespace()
        //{
        //    var allRepositorys = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Repository"));
        //    var registeredRepositorys = InstallerTestHelper.GetImplementationTypesFor(typeof(IRepository), containerWithRepositories);
        //    Assert.AreEqual(allRepositorys, registeredRepositorys);
        //}

        [Test]
        public void All_Repositories_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allRepositorys = GetPublicClassesFromApplicationAssembly(c => c.Is<IRepository>());
            var registeredRepositorys = InstallerTestHelper.GetImplementationTypesFor(typeof(IRepository), containerWithRepositories);
            Assert.AreEqual(allRepositorys, registeredRepositorys);
        }

        [Test]
        public void All_Repositories_are_transient()
        {
            var nonTransientRepositorys = InstallerTestHelper.GetHandlersFor(typeof(IRepository), containerWithRepositories)
                .Where(Repository => Repository.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.IsEmpty(nonTransientRepositorys);
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(RepositoryBase).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }

    }
}
