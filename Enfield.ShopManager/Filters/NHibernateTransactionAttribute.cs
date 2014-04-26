using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Controllers;

namespace Enfield.ShopManager.Filters
{
    public class NHibernateTransactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as RestrictedControllerBase;

            if (controller == null) return;

            controller.NHibernateSession.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as RestrictedControllerBase;

            if (controller == null)
                return;

            using (var session = controller.NHibernateSession)
            {
                if (session == null)
                    return;

                if (!session.Transaction.IsActive)
                    return;

                if (filterContext.Exception != null || !controller.ModelState.IsValid)
                    session.Transaction.Rollback();
                else
                    session.Transaction.Commit();
            }
        }
    }
}