using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Services
{
    public class LookupService : DomainServiceBase
    {
        public SelectList GetStateOptions(string selected)
        {
            var sizes = new List<string>(new string[] { "", "AR", "KY", "MO", "MS", "TN" });
            return new SelectList(sizes, selected);
        }

        public SelectList GetSizeOptions(int selected)
        {
            var sizes = new List<string>(new string[] { "25", "50", "75", "100" });
            return new SelectList(sizes, selected.ToString());
        }

        public SelectList GetSiteAccessOptions(string selected, bool includeAll = false)
        {
            var options = new List<string>(new string[] { "True", "False" });
            if (includeAll) options.Add("All");

            var sel = (string.IsNullOrWhiteSpace(selected)) ? "All" : selected;
            return new SelectList(options, sel);
        }

        public SelectList GetPaidOptions(string selected, bool includeAll = false)
        {
            var options = new List<string>(new string[] { "Paid", "Not Paid" });
            if (includeAll) options.Add("All");

            var sel = (string.IsNullOrWhiteSpace(selected)) ? "All" : selected;
            return new SelectList(options, sel);
        }

        public SelectList GetRoleOptions(string selected, bool includeAll = false)
        {
            var roles = new List<string>(new string[] { "Employee", "Manager", "Administrator" });
            if (includeAll) roles.Add("All");

            var sel = (string.IsNullOrWhiteSpace(selected)) ? "All" : selected;
            return new SelectList(roles, sel);
        }

        public SelectList GetLoginResultOptions(string selected, bool includeAll = false)
        {
            var options = new List<string>(new string[] { "Success", "Failure" });
            if (includeAll) options.Add("All");

            var sel = (string.IsNullOrWhiteSpace(selected)) ? "All" : selected;
            return new SelectList(options, sel);
        }

    }
}