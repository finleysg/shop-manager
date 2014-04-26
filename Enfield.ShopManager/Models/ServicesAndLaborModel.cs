using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class ServicesAndLaborModel
    {
        public ServicesAndLaborModel()
        {
            AccountTypes = new List<AccountTypeModel>();
        }

        public List<AccountTypeModel> AccountTypes { get; set; }
    }

    public class AccountTypeModel
    {
        public AccountTypeModel()
        {
            Services = new List<AccountTypeServiceModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal TaxRate { get; set; }
        public List<AccountTypeServiceModel> Services { get; set; }
    }

    public class AccountTypeServiceModel
    {
        public AccountTypeServiceModel()
        {
            Labor = new List<AccountTypeLaborModel>();
        }

        public AccountTypeModel Parent { get; set; }
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeDescription { get; set; }
        public List<AccountTypeLaborModel> Labor { get; set; }
    }

    public class AccountTypeLaborModel
    {
        public AccountTypeServiceModel Parent { get; set; }
        public int Id { get; set; }
        public int LaborTypeId { get; set; }
        public string LaborTypeDescription { get; set; }
    }
}