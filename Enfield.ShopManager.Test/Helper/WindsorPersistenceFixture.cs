using Castle.Windsor;
using Enfield.ShopManager.Plumbing;
using System.Web.Security;
using Castle.MicroKernel.Registration;
using System.IO;

namespace Enfield.ShopManager.Tests.Helper
{
    public class WindsorPersistenceFixture
    {
        private static IWindsorContainer container;
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitializeContainer()
        {
            container = new WindsorContainer().Install(new DomainServiceInstaller());
            container.Install(new RepositoryInstaller());
            container.Register(Component.For<RoleProvider>().ImplementedBy<FakeRoleProvider>());
            container.AddFacility<FakePersistenceFacility>();

            //FileInfo fileInfo = new FileInfo(@"C:\Projects\ShopManager\Enfield.ShopManager.Test\bin\Debug\Test.config");
            //log4net.Config.BasicConfigurator.Configure();
        }

        public static IWindsorContainer Container
        {
            get { return container; }
        }

        public static void DisposeContainer()
        {
            container.Dispose();
        }

    }
}
