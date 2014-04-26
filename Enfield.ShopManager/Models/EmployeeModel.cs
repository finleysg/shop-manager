using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName
        {
            get {
                if (Id == 100)
                    return "UNASSIGNED";
                else
                    return string.Format("{0} {1}", FirstName, LastName); 
            }
        }
    }
}