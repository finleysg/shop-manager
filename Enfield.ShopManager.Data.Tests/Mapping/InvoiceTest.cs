using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NUnit.Framework;
using Enfield.ShopManager.Data.Graph;
using FluentNHibernate.Testing;
using Enfield.ShopManager.Data.Tests.Fixtures;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    public class InvoiceTest
    {
        ISession session;
        ITransaction tx;

        [SetUp]
        public void Setup()
        {
            session = WindsorPersistenceFixture.Container.Resolve<ISession>();
            tx = session.BeginTransaction();
        }

        [TearDown]
        public void Cleanup()
        {
            tx.Rollback();
            tx.Dispose();
            session.Close();
        }

        [Test]
        public void Service_VerifyMap()
        {
            var invoice = session.Get<Invoice>(358600);
            var serviceType = session.Get<ServiceType>(29); //PIN STRIPE

            new PersistenceSpecification<Service>(session, new EnfieldEqualityComparer())
                .CheckProperty(s => s.ServiceDate, DateTime.Now)
                .CheckProperty(s => s.ModifyDate, DateTime.Now)
                .CheckProperty(s => s.ModifyUser, "Test")
                .CheckReference(s => s.ServiceType, serviceType)
                .CheckReference(s => s.Invoice, invoice)
                .VerifyTheMappings();
        }

        [Test]
        public void Labor_VerifyMap()
        {
            var invoice = session.Get<Invoice>(358600);
            var laborType = session.Get<LaborType>(1); //BUFF BAY
            var employee = session.Get<Employee>(10);

            new PersistenceSpecification<Labor>(session, new EnfieldEqualityComparer())
                .CheckProperty(s => s.LaborDate, DateTime.Now)
                .CheckProperty(s => s.ModifyDate, DateTime.Now)
                .CheckProperty(s => s.ModifyUser, "Test")
                .CheckReference(s => s.LaborType, laborType)
                .CheckReference(s => s.Invoice, invoice)
                .CheckReference(s => s.Employee, employee)
                .VerifyTheMappings();
        }

        [Test]
        public void Employee_VerifyMap()
        {
            new PersistenceSpecification<Employee>(session, new EnfieldEqualityComparer())
                .CheckProperty(e => e.FirstName, "John")
                .CheckProperty(e => e.IsEmployed, true)
                .CheckProperty(e => e.LastName, "Dough")
                .CheckProperty(e => e.ModifyDate, DateTime.Now)
                .CheckProperty(e => e.ModifyUser, "Test")
                .CheckProperty(e => e.Name, "JD")
                .CheckProperty(e => e.RoleName, "Employee")
                .VerifyTheMappings();
        }

        [Test]
        public void InvoiceHistory_VerifyMap()
        {
            var invoice = session.Get<Invoice>(358600);
                //.CheckReference(h => h.Invoice, invoice)

            new PersistenceSpecification<StockNumberHistory>(session, new EnfieldEqualityComparer())
                .CheckProperty(h => h.ModifyDate, DateTime.Now)
                .CheckProperty(h => h.ModifyUser, "Test")
                .CheckProperty(h => h.Note, "How now brown cow?")
                .CheckProperty(h => h.StockNumber, invoice.StockNumber)
                .CheckProperty(h => h.InvoiceId, 358600)
                .VerifyTheMappings();
        }

        [Test]
        public void Invoice_VerifyMap()
        {
            var account = session.Get<Account>(699);
            var location = session.Get<Location>(1);

            new PersistenceSpecification<Invoice>(session, new EnfieldEqualityComparer())
                .CheckProperty(i => i.Color, "Red")
                .CheckProperty(i => i.CompleteDate, DateTime.Now)
                .CheckProperty(i => i.IsComplete, false)
                .CheckProperty(i => i.IsPaid, true)
                .CheckProperty(i => i.Make, "Ford")
                .CheckProperty(i => i.Model, "Taurus")
                .CheckProperty(i => i.ModifyDate, DateTime.Now)
                .CheckProperty(i => i.ModifyUser, "Test")
                .CheckProperty(i => i.PurchaseOrderNumber, "135145")
                .CheckProperty(i => i.ReceiveDate, DateTime.Now)
                .CheckProperty(i => i.StockNumber, "yqy3yt43")
                .CheckProperty(i => i.VIN, "32tg345y5yggag24")
                .CheckProperty(i => i.WorkOrderNumber, "1")
                .CheckReference(i => i.Account, account)
                .CheckReference(i => i.Location, location)
                .VerifyTheMappings();
        }

        [Test]
        public void Invoice_CanAddService()
        {
            var invoice = session.Get<Invoice>(358600);
            var detail = session.Get<ServiceType>(44);

            invoice.AddService(
                new Service()
                {
                    ServiceDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    ModifyUser = "test",
                    ServiceType = detail
                });

            session.SaveOrUpdate(invoice);
            session.Flush();

            var invoice2 = session.Get<Invoice>(358600);
            Assert.AreEqual(invoice, invoice2);
            Assert.IsNotNull(invoice2.ServiceList.Where(s => s.ServiceType.Id == detail.Id).FirstOrDefault());
        }

        [Test]
        public void Invoice_CanAddLabor()
        {
            var invoice = session.Get<Invoice>(358600);
            var labor = session.Get<LaborType>(40);

            invoice.AddLabor(
                new Labor()
                {
                    LaborDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    ModifyUser = "test",
                    LaborType = labor
                });

            session.SaveOrUpdate(invoice);
            session.Flush();

            var invoice2 = session.Get<Invoice>(358600);
            Assert.AreEqual(invoice, invoice2);
            Assert.IsNotNull(invoice2.LaborList.Where(s => s.LaborType.Id == labor.Id).FirstOrDefault());
        }

        //[Test]
        //public void Invoice_CanAddHistory()
        //{
        //    var invoice = session.Get<Invoice>(358600);

        //    invoice.AddHistory(
        //        new StockNumberHistory()
        //        {
        //            Note = "Alas, poor Yorick",
        //            ModifyDate = DateTime.Now,
        //            ModifyUser = "test"
        //        });

        //    session.SaveOrUpdate(invoice);
        //    session.Flush();

        //    var invoice2 = session.Get<Invoice>(358600);
        //    Assert.AreEqual(invoice, invoice2);
        //    Assert.IsNotNull(invoice2.History.Where(h => h.Note == "Alas, poor Yorick").FirstOrDefault());
        //}


    }
}
