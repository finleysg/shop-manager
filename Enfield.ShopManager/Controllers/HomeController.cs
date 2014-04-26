using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Enfield.ShopManager.Data.Repository;

namespace Enfield.ShopManager.Controllers
{
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }
        public IRepositoryFactory RepositoryFactory { get; set; }

        public ActionResult Index()
        {
            ViewBag.Locations = RepositoryFactory.Create<SecurityRepository>().GetLocations();
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            Logger.Debug("Welcome to ASP.NET MVC!");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
