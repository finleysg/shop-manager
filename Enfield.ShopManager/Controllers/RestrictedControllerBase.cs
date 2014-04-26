using System.Web.Mvc;
using Enfield.ShopManager.Security;
using Enfield.ShopManager.Services;
using Castle.Core.Logging;
using Enfield.ShopManager.Models;
using NHibernate;
using Enfield.ShopManager.Filters;

namespace Enfield.ShopManager.Controllers
{
    [RequireSsl]
    [WriteToken]
    [NHibernateTransaction]
    public abstract class RestrictedControllerBase : Controller
    {
        public RestrictedControllerBase()
        {
            ViewBag.CompanyName = System.Configuration.ConfigurationManager.AppSettings["companyName"];
        }

        private ILogger logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        public ISession NHibernateSession { get; set; }
        public IDomainServiceFactory ServiceFactory { get; set; }
        public Token SecurityToken { get; set; }

        public string LocationName
        {
            get { return LocationServices.GetLocation(LocationId).Name; }
        }

        protected int LocationId
        {
            get { return SecurityToken.LocationId; }
        }

        protected bool IsInRole(RolesEnum role)
        {
            return SecurityToken.IsInRole(role);
        }

        #region | Service Accessors |

        protected LocationService LocationServices
        {
            get { return ServiceFactory.Create<LocationService>(); }
        }

        protected EmployeeService EmployeeServices
        {
            get { return ServiceFactory.Create<EmployeeService>(); }
        }

        protected InvoiceService InvoiceServices
        {
            get { return ServiceFactory.Create<InvoiceService>(); }
        }

        protected LookupService LookupServices
        {
            get { return ServiceFactory.Create<LookupService>(); }
        }

        protected SecurityService SecurityServices
        {
            get { return ServiceFactory.Create<SecurityService>(); }
        }

        protected AccountService AccountServices
        {
            get { return ServiceFactory.Create<AccountService>(); }
        }

        protected InvoiceNavigationService NavigationServices
        {
            get { return ServiceFactory.Create<InvoiceNavigationService>(); }
        }

        protected InvoiceAdministrationService AdministrationServices
        {
            get { return ServiceFactory.Create<InvoiceAdministrationService>(); }
        }

        protected AccountTypeService AccountTypeServices
        {
            get { return ServiceFactory.Create<AccountTypeService>(); }
        }

        protected ReportService ReportServices
        {
            get { return ServiceFactory.Create<ReportService>(); }
        }
        #endregion
    }
}