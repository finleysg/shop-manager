using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enfield.ShopManager.Security
{
    public class RequireSslAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Boolean IsRequired { get; set; }

        public RequireSslAttribute()
        {
            this.IsRequired = true;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.IsRequired &&
                filterContext.HttpContext.Request.Url.Scheme != "https" &&
                !filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.HttpContext.Response.Redirect(
                    filterContext.HttpContext.Request.Url.OriginalString.Replace("http", "https").Replace(":80", ""), true);
            }
            else if (!this.IsRequired &&
                     filterContext.HttpContext.Request.Url.Scheme == "https" &&
                     !filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.HttpContext.Response.Redirect(
                    filterContext.HttpContext.Request.Url.OriginalString.Replace("https", "http").Replace(":443", ""), true);
            }
        }
    }
}