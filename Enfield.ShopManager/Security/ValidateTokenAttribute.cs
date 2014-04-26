using System.Linq;
using System.Web.Mvc;
using Enfield.ShopManager.Controllers;
using System.Web.Routing;

namespace Enfield.ShopManager.Security
{
    public class ValidateTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public ValidateTokenAttribute(string roles)
        {
            Roles = roles;
        }

        //TODO: log failures
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                                     filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (!skipAuthorization)
            {
                var authCookie = filterContext.HttpContext.Request.Cookies["auth"];
                if (authCookie == null)
                {
                    RedirectToLogin(filterContext, "No auth cookie in request"); 
                    return;
                }

                var token = TokenSerializer.GetTokenFromCookie(authCookie);
                token.IpAddress = filterContext.HttpContext.Request.UserHostAddress;
                if (TokenHasher.IsExpired(token))
                {
                    RedirectToLogin(filterContext, "Token is expired");
                    return;
                }

                if (!TokenHasher.IsValid(token))
                {
                    RedirectToLogin(filterContext, string.Format("Token is invalid for {0}|{1}|{2} from {3}", token.UserId, token.RoleName, token.LocationId, token.IpAddress));
                    return;
                }

                if (!Roles.Split(',').Contains(token.RoleName))
                {
                    RedirectToLogin(filterContext, string.Format("{0} is an invalid role", token.RoleName));
                    return;
                }

                var controller = filterContext.Controller as RestrictedControllerBase;
                if (controller != null)
                {
                    controller.SecurityToken = token;
                    controller.Logger.DebugFormat("Authentication passed for {0} from {1}", token.UserId, token.IpAddress);
                }
            }
        }

        private void RedirectToLogin(AuthorizationContext filterContext, string reason)
        {
            var controller = filterContext.Controller as RestrictedControllerBase;
            if (controller != null)
            {
                controller.Logger.InfoFormat("Authentication failed: {0}", reason);
            }

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "Index" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                });
        }
    }
}