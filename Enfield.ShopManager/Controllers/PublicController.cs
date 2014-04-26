using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;

namespace Enfield.ShopManager.Controllers
{
    public class PublicController : Controller
    {
        public ActionResult Index()
        {
            var model = new PublicMenuModel("Home");
            model.BuildMenus();
            return View("Index", model);
        }

        public ActionResult Services()
        {
            var model = new PublicMenuModel("Services");
            model.BuildMenus();
            return View("Services", model);
        }

        public ActionResult Gallery()
        {
            var model = new PublicMenuModel("Gallery");
            model.BuildMenus();
            return View("Gallery", model);
        }

        public ActionResult Contact()
        {
            var model = new PublicMenuModel("Contact");
            model.BuildMenus();
            return View("Contact", model);
        }
    }
}
