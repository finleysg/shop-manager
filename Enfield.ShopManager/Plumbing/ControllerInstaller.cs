using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Enfield.ShopManager.Plumbing
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(FindControllers().Configure(c => c.LifeStyle.Transient));
        }

        private BasedOnDescriptor FindControllers()
        {
            return AllTypes.FromThisAssembly()
                .BasedOn<IController>()
                .If(Component.IsInSameNamespaceAs<Controllers.ShopFloorController>())
                .If(t => t.Name.EndsWith("Controller"));
        }
    }
}