using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Enfield.ShopManager.Services;

namespace Enfield.ShopManager.Plumbing
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRoleService>().ImplementedBy<RoleService>().LifeStyle.Transient,
                               Component.For<IMembershipService>().ImplementedBy<MembershipService>().LifeStyle.Transient);
        }
    }
}