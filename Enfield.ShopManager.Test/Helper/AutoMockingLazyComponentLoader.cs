using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.Registration;
using System.Collections;
using Rhino.Mocks;

namespace Enfield.ShopManager.Tests
{
    public class AutoMockingLazyComponentLoader : ILazyComponentLoader
    {
        public IRegistration Load(string key, Type service, IDictionary arguments)
        {
            return Component.For(service).Instance(MockRepository.GenerateMock(service, new Type[0]));
        }
    }
}
