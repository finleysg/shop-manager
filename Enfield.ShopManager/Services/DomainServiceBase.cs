using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Castle.Core.Logging;
using Enfield.ShopManager.Data.Repository;
using NHibernate;

namespace Enfield.ShopManager.Services
{
    public abstract class DomainServiceBase : IDomainService
    {
        private ILogger logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        public IRepositoryFactory RepositoryFactory { get; set; }

        #region | Factory Accessors |

        protected InvoiceRepository InvoiceRepository
        {
            get { return RepositoryFactory.Create<InvoiceRepository>(); }
        }

        protected AccountRepository AccountRepository
        {
            get { return RepositoryFactory.Create<AccountRepository>(); }
        }

        protected SecurityRepository SecurityRepository
        {
            get { return RepositoryFactory.Create<SecurityRepository>(); }
        }

        protected ReportRepository ReportRepository
        {
            get { return RepositoryFactory.Create<ReportRepository>(); }
        }

        protected AccountTypeRepository AccountTypeRepository
        {
            get { return RepositoryFactory.Create<AccountTypeRepository>(); }
        }
        #endregion

        protected List<Data.Graph.EmployeeLog> GetEmployeeLogFromCache(int locationId, bool forceRefresh = false)
        {
            var cacheKey = string.Format("signed-in-{0}", locationId);
            if (forceRefresh) HttpRuntime.Cache.Remove(cacheKey);

            var log = HttpRuntime.Cache[cacheKey] as List<Data.Graph.EmployeeLog>;
            if (forceRefresh || log == null || log.Count == 0)
            {
                log = SecurityRepository.GetEmployeeLog(new Data.Query.SignInQuery() { SignInDateStart = DateTime.Today, LocationId = locationId }).ToList();
                HttpRuntime.Cache.Insert(cacheKey, log, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12.0));
            }
            return log;
        }

        protected bool ValidatePassword(byte[] entered, byte[] stored)
        {
            if (entered.Length != stored.Length) return false;

            for (int i = 0; i < entered.Length; i++)
            {
                if (entered[i] != stored[i]) return false;
            }

            return true;
        }
    }
}