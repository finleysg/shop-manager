using Castle.Windsor;
using Enfield.ShopManager.Data.Tests.Fakes;

namespace Enfield.ShopManager.Data.Tests.Fixtures
{
    public class WindsorPersistenceFixture
    {
        private static IWindsorContainer container;

        public static void InitializeContainer()
        {
            container = new WindsorContainer();
            container.AddFacility<FakePersistenceFacility>(); ;
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
