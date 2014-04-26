using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Services
{
    public interface IDomainServiceFactory
    {
        TService Create<TService>() where TService : IDomainService;
    }
}
