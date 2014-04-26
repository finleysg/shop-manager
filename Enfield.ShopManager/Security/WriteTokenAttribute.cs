using System;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Controllers;

namespace Enfield.ShopManager.Security
{
    public class WriteTokenAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            var controller = filterContext.Controller as RestrictedControllerBase;
            if (controller != null && controller.SecurityToken != null)
            {
                var auth = TokenSerializer.GetCookieFromToken(RegenerateToken(controller.SecurityToken));
                if (filterContext.HttpContext.Request.IsLocal) //local development overrides
                {
                    controller.Logger.Info("Local development - not using a secure cookie");
                    auth.Domain = "localhost";
                    auth.Secure = false; 
                }
                filterContext.HttpContext.Response.Cookies.Add(auth);
            }
        }

        private Token RegenerateToken(Token token)
        {
            Token newToken = new Token()
            {
                CreateDate = DateTime.Now,
                IpAddress = token.IpAddress,
                LocationId = token.LocationId,
                Role = token.Role,
                UserId = token.UserId
            };
            TokenHasher.Hash(newToken);

            return newToken;
        }
    }
}