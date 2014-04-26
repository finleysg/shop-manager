using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class PayrollParamModel
    {
        public SelectList ActiveEmployees { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
    }
}