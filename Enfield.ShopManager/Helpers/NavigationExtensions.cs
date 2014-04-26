using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using System.Text;

namespace Enfield.ShopManager.Helpers
{
    public static class NavigationExtensions
    {
        public static MvcHtmlString VehicleDisplay(this HtmlHelper helper, InvoiceNavigationModel vehicle)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("GetInvoice", "ShopFloor", new { id = vehicle.Id.ToString() });

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div data-url=\"{0}\" class=\"shop-vehicle navigable ui-corner-all\" style=\"background-color: {1}; color: {2}\">",
                url,
                Formatter.GetVehicleColor(vehicle.Color),
                Formatter.GetVehicleTextColor(vehicle.Color));
            sb.AppendFormat("<p class=\"shop-vehicle-number\">{0}</p>", vehicle.Id);
            sb.AppendFormat("<p class=\"shop-vehicle-description\">{0} {1} {2}</p>", vehicle.Year, vehicle.Make, vehicle.Model);
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}