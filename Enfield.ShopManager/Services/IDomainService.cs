using Enfield.ShopManager.Data.Repository;

namespace Enfield.ShopManager.Services
{
    public interface IDomainService
    {
        IRepositoryFactory RepositoryFactory { get; set; }
    }
}
