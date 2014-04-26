using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class ReportHeaderModel
    {
        public ReportHeaderModel(string name)
        {
            ReportName = name;
            CompanyName = ConfigurationManager.AppSettings["companyName"];
            Url = ConfigurationManager.AppSettings["url"]; // "http://www.enfieldsdetail.com";
            Address = ConfigurationManager.AppSettings["address"];  //"2370 Covington Cove | Memphis, TN 38134";
            Phone = ConfigurationManager.AppSettings["phone"];  //"901.372.1560";
        }

        public string CompanyName { get; set; }
        public string ReportName { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}