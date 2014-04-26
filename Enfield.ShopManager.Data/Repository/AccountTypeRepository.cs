using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enfield.ShopManager.Data.Graph;
using NHibernate.Criterion;

namespace Enfield.ShopManager.Data.Repository
{
    public class AccountTypeRepository : RepositoryBase
    {

        public IList<AccountType> GetAccountTypes()
        {
            return Session.QueryOver<AccountType>().List();
        }

        public AccountType GetAccountType(int id)
        {
            return Session.QueryOver<AccountType>().Where(i => i.Id == id).SingleOrDefault();
        }

        public AccountType SaveAccountType(AccountType accountType)
        {
            Session.SaveOrUpdate(accountType);
            return accountType;
        }

        public IList<ServiceType> GetServiceTypes()
        {
            return Session.QueryOver<ServiceType>().List();
        }

        public ServiceType GetServiceType(int id)
        {
            return Session.QueryOver<ServiceType>().Where(i => i.Id == id).SingleOrDefault();
        }

        public ServiceType SaveServiceType(ServiceType serviceType)
        {
            Session.SaveOrUpdate(serviceType);
            return serviceType;
        }

        public IList<LaborType> GetLaborTypes()
        {
            return Session.QueryOver<LaborType>().List();
        }

        public LaborType GetLaborType(int id)
        {
            return Session.QueryOver<LaborType>().Where(i => i.Id == id).SingleOrDefault();
        }

        public LaborType SaveLaborType(LaborType laborType)
        {
            Session.SaveOrUpdate(laborType);
            return laborType;
        }

        public IList<AvailableServicesView> GetAvailableServices()
        {
            var services = Session.QueryOver<AvailableServicesView>()
                .OrderBy(o => o.Id).Asc
                .List();
            return services;
        }

        public IList<AvailableLaborView> GetAvailableLabor()
        {
            var labor = Session.QueryOver<AvailableLaborView>()
                .OrderBy(o => o.Id).Asc
                .List();
            return labor;
        }
    }
}
