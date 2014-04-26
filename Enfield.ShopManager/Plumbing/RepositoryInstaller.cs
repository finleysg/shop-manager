using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Enfield.ShopManager.Data.Repository;

namespace Enfield.ShopManager.Plumbing
{
    public class RepositoryInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<IRepositoryFactory>().AsFactory().LifeStyle.Transient);
            container.Register(FindRepositories().Configure(c => c.LifeStyle.Transient));
        }

        private BasedOnDescriptor FindRepositories()
        {
            return AllTypes.FromAssemblyContaining<IRepository>()
                .BasedOn<IRepository>()
                .If(Component.IsInSameNamespaceAs<RepositoryBase>())
                .If(t => t.Name.EndsWith("Repository"));
        }

    }
}