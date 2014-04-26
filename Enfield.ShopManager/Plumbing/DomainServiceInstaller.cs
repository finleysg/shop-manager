using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Enfield.ShopManager.Services;

namespace Enfield.ShopManager.Plumbing
{
    public class DomainServiceInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDomainServiceFactory>().AsFactory().LifeStyle.Transient);
            container.Register(FindDomainServices().Configure(c => c.LifeStyle.Transient));
        }

        private BasedOnDescriptor FindDomainServices()
        {
            return AllTypes.FromAssemblyContaining<IDomainService>()
                .BasedOn<DomainServiceBase>()
                .If(Component.IsInSameNamespaceAs<DomainServiceBase>())
                .If(t => t.Name.EndsWith("Service"));
        }
    }
}