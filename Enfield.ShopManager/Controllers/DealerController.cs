using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Enfield.ShopManager.Data.Repository;

namespace Enfield.ShopManager.Controllers
{
    public class DealerController : Controller
    {
        public ILogger Logger { get; set; }
        public IRepositoryFactory RepositoryFactory { get; set; }

        public ActionResult Index()
        {
            return InShop();
        }

        public ActionResult InShop()
        {
            return View("InShop");
        }

        public ActionResult History()
        {
            return View();
        }

    }
}
