using NHibernate;
using Castle.Core.Logging;

namespace Enfield.ShopManager.Data.Repository
{
    public abstract class RepositoryBase : IRepository
    {
        public RepositoryBase()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }
        public ISession Session { get; set; }
    }
}
