using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using System.Web.Routing;

namespace Enfield.ShopManager.Helpers
{
    public static class PagingExtensions
    {

        public static RouteValueDictionary GenerateInvoiceRoute(this InvoiceFilterModel model, int? page = null, int? size = null)
        {
            var receiveDateString = string.Empty;
            if (model.ReceivedDateStart.HasValue) receiveDateString = model.ReceivedDateStart.Value.ToShortDateString();

            return new RouteValueDictionary() {
                   { "Page", (page.HasValue) ? page.ToString() : model.Page.ToString() },
                   { "Size", (size.HasValue) ? size.Value.ToString() : model.Size.ToString() },
                   { "ReceivedDateStart", receiveDateString },
                   { "ReceivedDateEnd", model.ReceivedDateEnd.Value.ToShortDateString() },
                   { "LocationId", model.LocationId.Value.ToString() },
                   { "AccountName", model.AccountName },
                   { "ExcludeZeroTotal", model.ExcludeZeroTotal.ToString() },
                   { "HasBeenPaid", model.HasBeenPaid },
                   { "DoEvaluate", model.DoEvaluate.ToString() }
            };
        }

        public static RouteValueDictionary GenerateUserAccessRoute(this UserFilterModel model, int? page = null)
        {
            return new RouteValueDictionary() {
                   { "Page", (page.HasValue) ? page.Value.ToString() : model.Page.ToString() },
                   { "Size", model.Size.ToString() },
                   { "Name", model.Name },
                   { "Role", model.Role },
                   { "HasSiteAccess", model.HasSiteAccess }
            };
        }

        public static RouteValueDictionary GenerateUserAccessRouteForEdit(this UserFilterModel model, int id)
        {
            return new RouteValueDictionary() {
                   { "id", id.ToString() },
                   { "Page", model.Page.ToString() },
                   { "Size", model.Size.ToString() },
                   { "Name", model.Name },
                   { "Role", model.Role },
                   { "HasSiteAccess", model.HasSiteAccess }
            };
        }

        public static RouteValueDictionary GenerateSecurityLogRoute(this SecurityLogFilterModel model, int? page = null, int? size = null)
        {
            return new RouteValueDictionary() {
                   { "Page", (page.HasValue) ? page.ToString() : model.Page.ToString() },
                   { "Size", (size.HasValue) ? size.Value.ToString() : model.Size.ToString() },
                   { "LoginDateStart", model.LoginDateStart.Value.ToShortDateString() },
                   { "LoginDateEnd", model.LoginDateEnd.Value.ToShortDateString() },
                   { "LocationId", model.LocationId.Value.ToString() },
                   { "UserName", model.UserName },
                   { "ResultFlag", model.ResultFlag }
            };
        }

        public static RouteValueDictionary GenerateAccountListingRoute(this AccountFilterModel model, int? page = null)
        {
            return new RouteValueDictionary() {
                   { "Page", (page.HasValue) ? page.Value.ToString() : model.Page.ToString() },
                   { "Size", model.Size.ToString() },
                   { "Name", model.AccountName },
                   { "AccountTypeId", model.AccountTypeId }
            };
        }

        public static RouteValueDictionary GenerateAccountDetailRoute(this AccountFilterModel model, int id)
        {
            return new RouteValueDictionary() {
                   { "id", id.ToString() },
                   { "Page", model.Page.ToString() },
                   { "Size", model.Size.ToString() },
                   { "Name", model.AccountName },
                   { "AccountTypeId", model.AccountTypeId }
            };
        }

        public static RouteValueDictionary GenerateContactDetailRoute(this AccountFilterModel model, ContactModel contact)
        {
            return new RouteValueDictionary() {
                   { "id", contact.AccountId.ToString() },
                   { "contactId", contact.Id.ToString() },
                   { "Page", model.Page.ToString() },
                   { "Size", model.Size.ToString() },
                   { "Name", model.AccountName },
                   { "AccountTypeId", model.AccountTypeId }
            };
        }
    }
}