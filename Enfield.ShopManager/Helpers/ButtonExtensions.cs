using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

namespace Enfield.ShopManager.Helpers
{
    public static class ButtonExtensions
    {
        public static MvcHtmlString JqueryUiButton(this HtmlHelper helper, string id, string text, string icon)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='#' id='{0}' class='jq-button ui-state-default ui-corner-all'>", id);
            sb.AppendFormat("<span class='ui-icon ui-icon-{0}'></span>{1}</a>", icon, text);
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString JqueryUiButton(this HtmlHelper helper, string text, string icon, string action, string controller, RouteValueDictionary routeValues = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, routeValues);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='{0}' class='jq-button ui-state-default ui-corner-all'>", url);
            sb.AppendFormat("<span class='ui-icon ui-icon-{0}'></span>{1}</a>", icon, text);
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString JqueryAjaxButton(this HtmlHelper helper, string id, string text, string icon, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='#' id='{0}' data-url='{1}' class='jq-button ui-state-default ui-corner-all'>", id, url);
            sb.AppendFormat("<span class='ui-icon ui-icon-{0}'></span>{1}</a>", icon, text);
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString JquerySubmitButton(this HtmlHelper helper, string text, string icon)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<button type='submit' class='jq-button ui-state-default ui-corner-all'>");
            sb.AppendFormat("<span class='ui-icon ui-icon-{0}'></span>{1}</button>", icon, text);
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString JqueryCancelButton(this HtmlHelper helper, string action, string controller, RouteValueDictionary routeValues)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, routeValues);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='{0}' class='jq-button ui-state-default ui-corner-all'>", url);
            sb.Append("<span class='ui-icon ui-icon-close'></span>Cancel</a>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString JqueryNewButton(this HtmlHelper helper, string text, string icon, string action, string controller, RouteValueDictionary routeValues)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, routeValues);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='{0}' class='jq-button ui-state-default ui-corner-all'>", url);
            sb.AppendFormat("<span class='ui-icon ui-icon-{0}'></span>{1}</a>", icon, text);
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString AutoChangeDropDownList(this HtmlHelper helper, string id, List<SelectListItem> items, string action)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<select id='{0}'>", id);
            foreach(var item in items)
            {
                var url = urlHelper.Action(action, new { id = item.Value });
                if (item.Selected)
                    sb.AppendFormat("<option selected='selected' value='{0}' data-url='{1}'>{2}</option>", item.Value, url, item.Text);
                else
                    sb.AppendFormat("<option value='{0}' data-url='{1}'>{2}</option>", item.Value, url, item.Text);
            }
            sb.Append("</select>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString JqueryPrintButton(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='javascript:window.print()' id='print-me' class='print-button'>");
            sb.Append("<span class='ui-icon ui-icon-print'></span>Print Report</a>");
            return MvcHtmlString.Create(sb.ToString());
        }

    }
}