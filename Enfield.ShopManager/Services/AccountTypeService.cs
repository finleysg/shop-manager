using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Services
{
    public class AccountTypeService : DomainServiceBase
    {
        private const int All = -1;

        public List<SelectListItem> GetAccountTypes(bool includeAll = false, int? selected = null)
        {
            var accountTypes = AccountTypeRepository.GetAccountTypes().OrderBy(o => o.Description);

            List<SelectListItem> lookup =
                accountTypes.Select(l => new SelectListItem() { Value = l.Id.ToString(), Text = l.Description }).ToList();

            if (includeAll)
            {
                lookup.Add(new SelectListItem() { Value = All.ToString(), Text = "ALL" });
            }

            if (selected.HasValue)
            {
                var selectedItem = lookup.Where(l => l.Value == selected.Value.ToString()).FirstOrDefault();
                if (selectedItem != null) selectedItem.Selected = true;
            }

            return lookup;
        }

        public SelectList GetServiceTypes()
        {
            var serviceTypes = AccountTypeRepository.GetServiceTypes().OrderBy(o => o.Description);
            return new SelectList(serviceTypes, "Id", "Description");
        }

        public SelectList GetLaborTypes()
        {
            var laborTypes = AccountTypeRepository.GetLaborTypes().OrderBy(o => o.Description);
            return new SelectList(laborTypes, "Id", "Description");
        }

        public List<AccountTypeModel> GetServicesAndLabor()
        {
            var accountTypes = AccountTypeRepository.GetAccountTypes().OrderBy(o => o.Description);

            List<AccountTypeModel> tree = new List<AccountTypeModel>();
            foreach (var accountType in accountTypes)
            {
                AccountTypeModel anode = new AccountTypeModel();
                anode.Id = accountType.Id;
                anode.Description = accountType.Description;
                anode.TaxRate = (decimal)accountType.TaxRate;

                foreach (var service in accountType.ServiceTypeList.OrderBy(o => o.ServiceType.Description))
                {
                    AccountTypeServiceModel snode = new AccountTypeServiceModel();
                    snode.Parent = anode;
                    snode.Id = service.Id;
                    snode.ServiceTypeId = service.ServiceType.Id;
                    snode.ServiceTypeDescription = service.ServiceType.Description;
                    anode.Services.Add(snode);

                    foreach (var labor in service.LaborTypeList.OrderBy(o => o.LaborType.Description))
                    {
                        AccountTypeLaborModel lnode = new AccountTypeLaborModel();
                        lnode.Parent = snode;
                        lnode.Id = labor.Id;
                        lnode.LaborTypeId = labor.LaborType.Id;
                        lnode.LaborTypeDescription = labor.LaborType.Description;
                        snode.Labor.Add(lnode);
                    }
                }
                tree.Add(anode);
            }
            return tree;
        }

        public void AddAccountType(string description)
        {
            //TODO
        }

        public void AddServiceType(string description)
        {
            var existing = AccountTypeRepository.GetServiceTypes();
            if (null != existing.Where(e => e.Description == description).FirstOrDefault())
            {
                throw new InvalidOperationException(string.Format("{0} already exists as a service type", description));
            }
            AccountTypeRepository.SaveServiceType(new ServiceType() { Description = description });
        }

        public void AddLaborType(string description)
        {
            var existing = AccountTypeRepository.GetLaborTypes();
            if (null != existing.Where(e => e.Description == description).FirstOrDefault())
            {
                throw new InvalidOperationException(string.Format("{0} already exists as a labor type", description));
            }
            AccountTypeRepository.SaveLaborType(new LaborType() { Description = description });
        }

        public void AddServiceToAccountType(int accountTypeId, int serviceTypeId)
        {
            var accountType = AccountTypeRepository.GetAccountType(accountTypeId);
            if (accountType == null) throw new ArgumentException("accountTypeId");

            var serviceType = AccountTypeRepository.GetServiceType(serviceTypeId);
            if (serviceType == null) throw new ArgumentException("serviceTypeId");

            var newService = new Data.Graph.AccountTypeService();
            newService.ServiceType = serviceType;
            newService.DefaultEstimatedTime = 0;
            newService.DefaultRate = 0;
            newService.IsActive = true;
            accountType.AddService(newService);

            AccountTypeRepository.SaveAccountType(accountType);
        }

        public void AddLaborToAccountType(int accountTypeId, int accountTypeServiceId, int laborTypeId)
        {
            var accountType = AccountTypeRepository.GetAccountType(accountTypeId);
            if (accountType == null) throw new ArgumentException("accountTypeId");

            var service = accountType.ServiceTypeList.Where(s => s.Id == accountTypeServiceId).FirstOrDefault();
            if (service == null) throw new ArgumentException("accountTypeServiceId");

            var laborType = AccountTypeRepository.GetLaborType(laborTypeId);
            if (laborType == null) throw new ArgumentException("laborTypeId");

            var newLabor = new AccountTypeLabor();
            newLabor.DefaultRate = 0;
            newLabor.DefaultRateType = "F";
            newLabor.LaborType = laborType;
            service.AddLabor(newLabor);

            AccountTypeRepository.SaveAccountType(accountType);
        }

        public void RemoveServiceFromAccountType(int accountTypeId, int accountTypeServiceId)
        {
            var accountType = AccountTypeRepository.GetAccountType(accountTypeId);
            if (accountType == null) throw new ArgumentException("accountTypeId");

            var service = accountType.ServiceTypeList.Where(s => s.Id == accountTypeServiceId).FirstOrDefault();
            if (service == null) throw new ArgumentException("accountTypeServiceId");

            accountType.ServiceTypeList.Remove(service);

            AccountTypeRepository.SaveAccountType(accountType);
        }

        public void RemoveLaborFromAccountType(int accountTypeId, int accountTypeServiceId, int accountTypeLaborId)
        {
            var accountType = AccountTypeRepository.GetAccountType(accountTypeId);
            if (accountType == null) throw new ArgumentException("accountTypeId");

            var service = accountType.ServiceTypeList.Where(s => s.Id == accountTypeServiceId).FirstOrDefault();
            if (service == null) throw new ArgumentException("accountTypeServiceId");

            var labor = service.LaborTypeList.Where(l => l.Id == accountTypeLaborId).FirstOrDefault();
            if (labor == null) throw new ArgumentException("accountTypeLaborId");

            service.LaborTypeList.Remove(labor);

            AccountTypeRepository.SaveAccountType(accountType);
        }

        public void UpdateServiceType(int id, string description)
        {
            var existing = AccountTypeRepository.GetServiceType(id);
            if (null == existing)
            {
                throw new ArgumentException(string.Format("{0} is not a valid service type id", id));
            }
            //TODO: validate description
            existing.Description = description;
            AccountTypeRepository.SaveServiceType(existing);
        }

        public void UpdateLaborType(int id, string description)
        {
            var existing = AccountTypeRepository.GetLaborType(id);
            if (null == existing)
            {
                throw new ArgumentException(string.Format("{0} is not a valid labor type id", id));
            }
            //TODO: validate description
            existing.Description = description;
            AccountTypeRepository.SaveLaborType(existing);
        }
    }
}