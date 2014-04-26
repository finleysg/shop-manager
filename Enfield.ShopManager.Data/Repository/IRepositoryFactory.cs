using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Repository
{
    public interface IRepositoryFactory
    {
        TRepository Create<TRepository>() where TRepository : IRepository;
    }
}
