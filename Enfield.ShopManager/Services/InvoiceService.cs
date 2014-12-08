using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using AutoMapper;
using Enfield.ShopManager.Models;
using System.Reflection;
using System.Web.Mvc;

namespace Enfield.ShopManager.Services
{
    public class InvoiceService : DomainServiceBase
    {
        private const int defaultEmployeeId = 100;
        private const string defaultEmployeeName = "-NONE-";
        private Data.Graph.Employee defaultEmployee;

        public InvoiceService()
        {
            defaultEmployee = new Data.Graph.Employee(defaultEmployeeId);
            defaultEmployee.Name = defaultEmployeeName;
        }

        #region | Invoice |

        public InvoiceModel GetInvoice(int invoiceId)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) return null;

            return Mapper.Map<Data.Graph.Invoice, InvoiceModel>(invoice);
        }

        public InvoiceModel GetInvoice(string vin)
        {
            var invoices = InvoiceRepository.GetInvoices(new Data.Query.InvoiceQuery() { VIN = vin });
            if (invoices == null || invoices.Count == 0) return null;

            return GetInvoice(invoices.First().Id);
        }

        public InvoiceModel CreateInvoice(NewInvoiceModel invoice, int locationId)
        {
            Data.Graph.Invoice inv = new Data.Graph.Invoice();

            var account = AccountRepository.GetAccount(invoice.AccountId);
            if (account == null)
            {
                account = new Data.Graph.Account();
                account.Name = invoice.AccountName;
                account.AccountType = AccountTypeRepository.GetAccountType(invoice.AccountTypeId);
                AccountRepository.SaveAccount(account);
            }

            inv.Account = account;
            inv.Color = invoice.Color;
            inv.Location = new Data.Graph.Location(locationId);
            inv.Make = invoice.Make;
            inv.Model = invoice.Model;
            inv.ReceiveDate = DateTime.Now;
            inv.VIN = invoice.VIN;
            inv.Year = invoice.Year;
            inv.TaxRate = (decimal)account.AccountType.TaxRate;

            InvoiceRepository.SaveInvoice(inv);
            Logger.InfoFormat("Created new invoice {0} for {1} at location id {2}", inv.Id, inv.Account.Name, locationId);

            return Mapper.Map<Data.Graph.Invoice, InvoiceModel>(inv);
        }

        public InvoiceModel CompleteInvoice(int invoiceId)
        {
            var inv = InvoiceRepository.GetInvoice(invoiceId);
            if (inv == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            inv.CompleteDate = DateTime.Now;
            inv.IsComplete = true;
            InvoiceRepository.SaveInvoice(inv);
            Logger.InfoFormat("Completed invoice {0} at {1}", inv.Id, inv.Location.Name);

            if (!string.IsNullOrEmpty(inv.VIN))
            {
                var history = new Data.Graph.StockNumberHistory();
                history.InvoiceId = invoiceId;
                history.StockNumber = inv.VIN;
                history.Note = "Invoice completed.";
                InvoiceRepository.SaveHistory(history);
            }

            return Mapper.Map<Data.Graph.Invoice, InvoiceModel>(inv);
        }

        public InvoiceModel RecallInvoice(int invoiceId)
        {
            var inv = InvoiceRepository.GetInvoice(invoiceId);
            if (inv == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            inv.IsComplete = false;
            InvoiceRepository.SaveInvoice(inv);
            Logger.InfoFormat("Recalled invoice {0} at {1}", inv.Id, inv.Location.Name);

            if (!string.IsNullOrEmpty(inv.VIN))
            {
                var history = new Data.Graph.StockNumberHistory();
                history.InvoiceId = invoiceId;
                history.StockNumber = inv.VIN;
                history.Note = "Invoice recalled.";
                InvoiceRepository.SaveHistory(history);
            }

            return Mapper.Map<Data.Graph.Invoice, InvoiceModel>(inv);
        }

        public InvoiceModel DeleteInvoice(int invoiceId)
        {
            var inv = InvoiceRepository.GetInvoice(invoiceId);
            if (inv == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            InvoiceRepository.DeleteInvoice(inv);
            Logger.InfoFormat("Deleted invoice {0} at {1}", inv.Id, inv.Location.Name);

            return Mapper.Map<Data.Graph.Invoice, InvoiceModel>(inv);
        }

        public InvoiceModel UpdateAccount(int id, int accountId)
        {
            var invoice = InvoiceRepository.GetInvoice(id);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", id));

            var originalAccount = invoice.Account.Name;
            invoice.Account = AccountRepository.GetAccount(accountId);
            if (invoice.Account == null) throw new ArgumentException(string.Format("{0} is an invalid account id", accountId));

            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Changed account from {0} to {1} on invoice {2} at {3}", originalAccount, invoice.Account.Name, invoice.Id, invoice.Location.Name);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        public InvoiceModel UpdateInvoice(int id, string field, object value)
        {
            var invoice = InvoiceRepository.GetInvoice(id);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", id));

            PropertyInfo prop = invoice.GetType().GetProperty(field, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(invoice, value, null);
                InvoiceRepository.SaveInvoice(invoice);
                Logger.InfoFormat("Updated {0} field to {1} on invoice {2} at {3}", field, value, id, invoice.Location.Name);
            }
            else
            {
                Logger.WarnFormat("Could not find field {0} on invoice {1}", field, id);
            }

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        #endregion

        #region | Services Tabs |

        public ServicesModel GetServices(int locationId, InvoiceModel invoice)
        {
            ServicesModel model = new ServicesModel();
            model.AvailableLabor = GetAvailableLaborTypes(invoice);
            model.AvailableServices = GetAvailableServiceTypes(invoice);
            model.SignedInEmployees = GetSignedInEmployees(locationId);
            return model;
        }

        public List<EmployeeModel> GetSignedInEmployees(int locationId = 1, bool forceRefresh = false)
        {
            var log = GetEmployeeLogFromCache(locationId, forceRefresh);
            var model = log.Select(l => l.Employee).ToList();

            return Mapper.Map<List<Data.Graph.Employee>, List<EmployeeModel>>(model);
        }

        public SelectList GetSignedInEmployeeSelectList(int locationId = 1)
        {
            var employees = GetSignedInEmployees(locationId, forceRefresh: true);
            return new SelectList(employees, "Id", "DisplayName");
        }

        private List<Data.Graph.AvailableServicesView> AvailableServiceTypes
        {
            get
            {
                var availableServices = HttpRuntime.Cache["available-services"] as List<Data.Graph.AvailableServicesView>;
                if (availableServices == null || availableServices.Count == 0)
                {
                    Logger.Info("Reloading available services from database");
                    availableServices = AccountTypeRepository.GetAvailableServices().OrderBy(o => o.ServiceTypeDescription).ToList();
                    HttpRuntime.Cache.Insert("available-services", availableServices, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12.0));
                }
                return availableServices;
            }
        }

        private List<ServiceTypeModel> GetAvailableServiceTypes(InvoiceModel invoice)
        {
            if (invoice == null) return new List<ServiceTypeModel>();

            return AvailableServiceTypes
                .Where(s => s.AccountTypeDescription == invoice.AccountType)
                .Select(s => new ServiceTypeModel() { Id = s.ServiceTypeId, Description = s.ServiceTypeDescription }).ToList();
        }

        private List<Data.Graph.AvailableLaborView> AvailableLaborTypes
        {
            get
            {
                var availableLabor = HttpRuntime.Cache["available-labor"] as List<Data.Graph.AvailableLaborView>;
                if (availableLabor == null || availableLabor.Count == 0)
                {
                    Logger.Info("Reloading available labor from database");
                    availableLabor = AccountTypeRepository.GetAvailableLabor().ToList();
                    HttpRuntime.Cache.Insert("available-labor", availableLabor, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12.0));
                }
                return availableLabor;
            }
        }

        private List<LaborTypeModel> GetAvailableLaborTypes(InvoiceModel invoice)
        {
            if (invoice == null) return new List<LaborTypeModel>();

            var serviceTypes = invoice.ServiceList.Select(s => s.ServiceTypeDescription);
            return AvailableLaborTypes
                .Where(l => serviceTypes.Contains(l.ServiceTypeDescription))
                .Select(l => new LaborTypeModel() { Id = l.LaborTypeId, Description = l.LaborTypeDescription }).ToList();
        }

        public void ClearAvailableTypeCache()
        {
            HttpRuntime.Cache.Remove("available-services");
            HttpRuntime.Cache.Remove("available-labor");
        }

        #endregion

        #region | Service |

        public InvoiceModel AddService(int invoiceId, int serviceTypeId, decimal rate)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            var serviceType = AvailableServiceTypes.Where(s => s.ServiceTypeId == serviceTypeId).FirstOrDefault();
            if (serviceType == null) throw new ArgumentException(string.Format("{0} is an invalid service type id"));

            var service = new Data.Graph.Service();
            service.ServiceDate = DateTime.Now;
            service.ServiceType = new Data.Graph.ServiceType(serviceType);
            service.Rate = rate;
            invoice.AddService(service);

            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Added service type id {0} to invoice {1}", serviceTypeId, invoiceId);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        public InvoiceModel DeleteService(int invoiceId, int serviceId)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            var service = invoice.ServiceList.Where(s => s.Id == serviceId).FirstOrDefault();
            if (service == null) throw new ArgumentException(string.Format("{0} is an invalid service id", serviceId));

            invoice.ServiceList.Remove(service);
            InvoiceRepository.SaveInvoice(invoice);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        public InvoiceModel UpdateServiceRate(int id, int serviceId, decimal rate)
        {
            var invoice = InvoiceRepository.GetInvoice(id);
            invoice.ServiceList.Where(s => s.Id == serviceId).First().Rate = rate;
            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Changed service rate for service id {0} to {1} on invoice {2}", serviceId, rate, id);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        #endregion

        #region | Labor |

        public InvoiceModel AddLabor(int invoiceId, int laborTypeId, decimal rate)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            var labor = new Data.Graph.Labor();
            labor.LaborDate = DateTime.Now;
            labor.LaborType = new Data.Graph.LaborType(AvailableLaborTypes.Where(t => t.LaborTypeId == laborTypeId).First());
            labor.EstimatedRate = rate;
            labor.Employee = defaultEmployee;

            invoice.AddLabor(labor);
            invoice.CalculateLaborRates(labor.LaborType.Id);

            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Added labor type id {0} to invoice {1} with a base rate of {2}", laborTypeId, invoiceId, rate);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        public InvoiceModel UpdateLabor(int invoiceId, int laborId, int employeeId, int locationId)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            var labor = invoice.LaborList.Where(l => l.Id == laborId).FirstOrDefault();
            if (labor == null) throw new ArgumentException("laborId");

            var employee = GetEmployeeLogFromCache(locationId).Where(e => e.Employee.Id == employeeId).Select(s => s.Employee).FirstOrDefault();
            if (employee == null) throw new ArgumentException("employeeId");

            if (labor.Employee.Id == defaultEmployeeId)
            {
                labor.Employee = employee;
            }
            else
            {
                var newLabor = new Data.Graph.Labor();
                newLabor.LaborDate = DateTime.Now;
                newLabor.LaborType = new Data.Graph.LaborType(AvailableLaborTypes.Where(t => t.LaborTypeId == labor.LaborType.Id).First());
                newLabor.EstimatedRate = labor.EstimatedRate;
                newLabor.Employee = employee;
                invoice.AddLabor(newLabor);
            }
            invoice.CalculateLaborRates(labor.LaborType.Id);

            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Added employee id {0} to labor id {1} on invoice {2}", employeeId, laborId, invoiceId);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        public InvoiceModel DeleteLabor(int invoiceId, int laborId)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            var labor = invoice.LaborList.Where(l => l.Id == laborId).FirstOrDefault();
            if (labor == null) throw new ArgumentException("laborId");

            invoice.LaborList.Remove(labor);
            invoice.CalculateLaborRates(labor.LaborType.Id);

            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Deleted labor id {0} from invoice {1}", laborId, invoiceId);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        // TODO: test?
        public InvoiceModel UpdateLaborRate(int id, int laborId, decimal rate)
        {
            var invoice = InvoiceRepository.GetInvoice(id);
            invoice.LaborList.Where(s => s.Id == laborId).First().ActualRate = rate;
            InvoiceRepository.SaveInvoice(invoice);
            Logger.InfoFormat("Updated labor rate to {0} on labor id {1} on invoice id {2}", rate, laborId, id);

            return Mapper.Map<Data.Graph.Invoice, Models.InvoiceModel>(invoice);
        }

        #endregion

        #region | History |

        public List<HistoryModel> GetVinHistory(InvoiceModel invoice)
        {
            var history = InvoiceRepository.GetInvoiceHistory(invoice.VIN);
            return Mapper.Map<IList<Data.Graph.StockNumberHistory>, List<HistoryModel>>(history);
        }

        public List<HistoryModel> AddHistory(int invoiceId, string note)
        {
            var invoice = InvoiceRepository.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException(string.Format("{0} is an invalid invoice number", invoiceId));

            if (!string.IsNullOrEmpty(invoice.VIN))
            {
                var history = new Data.Graph.StockNumberHistory()
                {
                    Note = note,
                    InvoiceId = invoice.Id,
                    StockNumber = invoice.VIN
                };
                InvoiceRepository.SaveHistory(history);
            }

            var returnHistory = InvoiceRepository.GetInvoiceHistory(invoice.VIN);

            return Mapper.Map<IList<Data.Graph.StockNumberHistory>, List<HistoryModel>>(returnHistory);
        }

        #endregion

    }
}