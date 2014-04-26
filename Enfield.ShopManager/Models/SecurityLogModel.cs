using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class SecurityLogModel
    {
        public int LoginAttemptId { get; set; }
        public DateTime LoginDate { get; set; }
        public bool ResultFlag { get; set; }
        public string LocationName { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }
    }
}