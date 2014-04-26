using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using System.Linq;

namespace Enfield.ShopManager.Models
{

    public class LaborModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string LaborTypeDescription { get; set; }
        public decimal ActualRate { get; set; }
        public int ActualTime { get; set; }
    }

    public class ServiceModel
    {
        public int Id { get; set; }
        public string ServiceTypeDescription { get; set; }
        public decimal Rate { get; set; }
        public int EstimatedTime { get; set; }
    }

    public class HistoryModel
    {
        public int Id { get; set; }
        public string StockNumber { get; set; }
        public int InvoiceId { get; set; }
        public string Note { get; set; }
        public string ModifyUser { get; set; }
        public DateTime ModifyDate { get; set; }
    }

    public class InvoiceListingModel
    {
        public InvoiceFilterModel Filter { get; set; }
        public IPagedList<InvoiceTotalModel> InvoiceList { get; set; }
    }

    public class InvoiceAdministrationModel
    {
        public InvoiceFilterModel Filter { get; set; }
        public InvoiceModel CurrentInvoice { get; set; }
        public IPagedList<int> InvoiceList { get; set; }
    }
}