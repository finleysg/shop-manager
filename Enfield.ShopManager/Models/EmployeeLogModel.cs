using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class EmployeeLogModel
    {
        public string WorkDate { get; set; }
        public List<EmployeeSignInModel> EmployeeList { get; set; }
    }
}