using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class ServiceTreeModel
    {
        public ServiceTreeModel()
        {
            attr = new ServiceTreeAttribute();
            children = new List<ServiceTreeModel>();
        }

        public string data { get; set; }
        public ServiceTreeAttribute attr { get; set; }
        public List<ServiceTreeModel> children { get; set; }
    }

    public class ServiceTreeAttribute
    {
        public string id { get; set; }
        public bool selected { get; set; }
    }
}