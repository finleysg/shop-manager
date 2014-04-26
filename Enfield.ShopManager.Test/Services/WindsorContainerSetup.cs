using Enfield.ShopManager.Tests.Helper;
using NUnit.Framework;
using Enfield.ShopManager.Mapping;

namespace Enfield.ShopManager.Tests.Services
{
    [SetUpFixture]
    public class WindsorContainerSetup
    {

        [SetUp]
        public void ControllerInstallerSetup()
        {
            WindsorPersistenceFixture.InitializeContainer();
            MapConfiguration.Bootstrap();
        }

        [TearDown]
        public void ControllerInstallerTeardown()
        {
            WindsorPersistenceFixture.DisposeContainer();
        }
    }
}
