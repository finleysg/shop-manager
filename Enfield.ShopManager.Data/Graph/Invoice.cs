using System;
using System.Collections.Generic;
using System.Linq;
using Enfield.ShopManager.Data.Map;

namespace Enfield.ShopManager.Data.Graph
{
    public class Invoice : AutoMapBase<Invoice>, Audit.IHaveAuditInformation
    {
        public virtual IList<Labor> LaborList { get; set; }
        public virtual IList<Service> ServiceList { get; set; }
        public virtual Account Account { get; set; }
        public virtual DateTime ReceiveDate { get; set; }
        public virtual DateTime? CompleteDate { get; set; }

        private string stockNumber;
        public virtual string StockNumber
        {
            get { return stockNumber; }
            set { stockNumber = (value == null) ? null : value.ToUpper(); }
        }

        private string vin;
        public virtual string VIN 
        {
            get { return vin; }
            set { vin = (value == null) ? null : value.ToUpper(); }
        }

        public virtual string Year { get; set; }

        private string make;
        public virtual string Make
        {
            get { return make; }
            set { make = (value == null) ? null : value.ToUpper(); }
        }

        private string model;
        public virtual string Model
        {
            get { return model; }
            set { model = (value == null) ? null : value.ToUpper(); }
        }

        private string color;
        public virtual string Color
        {
            get { return color; }
            set { color = (value == null) ? null : value.ToUpper(); }
        }

        public virtual bool IsComplete { get; set; }
        public virtual bool IsPaid { get; set; }
        public virtual string PurchaseOrderNumber { get; set; }
        public virtual string WorkOrderNumber { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual Location Location { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual int InvoiceTypeId { get; set; }

        [DoNotMap]
        public virtual decimal ServiceTotal
        {
            get { return ServiceList.Sum(s => s.Rate); }
        }

        public Invoice()
        {
            InvoiceTypeId = 1;
            LaborList = new List<Labor>();
            ServiceList = new List<Service>();
            //History = new List<StockNumberHistory>();
        }

        public virtual void AddLabor(Labor child)
        {
            child.Invoice = this;
            LaborList.Add(child);
        }

        public virtual void AddService(Service child)
        {
            child.Invoice = this;
            ServiceList.Add(child);
        }

        public virtual void CalculateLaborRates(int laborTypeId)
        {
            // recalculate labor rates from remaining like tasks (if any)
            var commonLabor = LaborList.Where(l => l.LaborType.Id == laborTypeId);
            if (commonLabor.Count() > 0)
            {
                var rate = commonLabor.Where(l => l.EstimatedRate > 0).Select(r => r.EstimatedRate).FirstOrDefault();
                if (rate <= 0)
                {
                    //TODO: log warning
                    rate = 10;
                }

                foreach (Labor l in commonLabor)
                {
                    l.ActualRate = (rate / commonLabor.Count()) * l.Employee.Rate;
                }
            }

        }
    }
}
