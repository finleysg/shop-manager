using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class ServicesModel
    {
        public List<ServiceTypeModel> AvailableServices { get; set; }
        public List<LaborTypeModel> AvailableLabor { get; set; }
        public List<EmployeeModel> SignedInEmployees { get; set; }
    }
}