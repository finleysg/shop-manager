using NHibernate;

namespace Enfield.ShopManager.Data.Repository
{
    public interface IRepository
    {
        ISession Session { get; set; }
    }
}
