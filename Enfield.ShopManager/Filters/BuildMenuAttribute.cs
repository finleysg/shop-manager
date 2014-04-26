using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Controllers;

namespace Enfield.ShopManager.Filters
{
    public class BuildMenuAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //skip requests (json) without a model
            if (filterContext.Controller.ViewData.Model == null) return;

            var actionName = filterContext.ActionDescriptor.ActionName;
            if (actionName == "Index") return;

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var controller = filterContext.Controller as RestrictedControllerBase;

            MenuModel menu = MenuModel.Create(controller.ViewData.Model, controllerName, actionName, controller.SecurityToken.RoleName);
            menu.BuildMenus();

            controller.ViewBag.Menu = menu;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as RestrictedControllerBase;
            if (controller != null) controller.ViewBag.Location = controller.LocationName;
        }
    }
}