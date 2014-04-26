using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Enfield.ShopManager.Plumbing;
using Castle.Facilities.TypedFactory;
using Enfield.ShopManager.Filters;

namespace Enfield.ShopManager
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogErrorAttribute() { 
                View = "Error",
                Order = 10
            });
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");

            routes.MapRoute(
                "Login", // Route name
                "Login/{action}", // URL with parameters
                new { controller = "Login", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Public", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Shop", // Route name
                "{controller}/{action}/{id}/{stocknumber}", // URL with parameters
                new { controller = "ShopFloor", action = "GetInvoice", stocknumber = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Reports",
                "Reporting/{action}",
                new { controller = "Reporting", action = "Invoice" }
            );
        }

        private static void BootstrapContainer()
        {
            container = new WindsorContainer();
            container.Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_Start()
        {
            BootstrapContainer();

            Mapping.MapConfiguration.Bootstrap();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}