using System.Net.Mime;
using System.Web.Mvc;
using Enfield.ShopManager.Helpers;

namespace Enfield.ShopManager.Filters
{
    public class JsonErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var response = filterContext.RequestContext.HttpContext.Response;
            response.Write(filterContext.Exception.Message);
            response.ContentType = MediaTypeNames.Text.Plain;
            response.StatusCode = 500;
            filterContext.ExceptionHandled = true;
        }
    }
}