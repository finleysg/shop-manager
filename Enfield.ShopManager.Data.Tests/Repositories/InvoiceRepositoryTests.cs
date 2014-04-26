using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enfield.ShopManager.Data.Repository;
using Enfield.ShopManager.Data.Graph;
using NHibernate;
using Enfield.ShopManager.Data.Query;
using Enfield.ShopManager.Data.Tests.Fixtures;

namespace Enfield.ShopManager.Data.Tests.Repositories
{
    public class InvoiceRepositoryTests
    {
        ISession session;
        ITransaction tx;
        InvoiceRepository repository;
        AccountRepository accountRepository;


        [SetUp]
        public void Setup()
        {
            session = WindsorPersistenceFixture.Container.Resolve<ISession>();

            repository = new InvoiceRepository();
            repository.Session = session;
            accountRepository = new AccountRepository();
            accountRepository.Session = session;

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
        public void GetInvoice_ReturnBlueFordEscape()
        {
            var invoice = repository.GetInvoice(358750);
            Assert.IsNotNull(invoice);
            Assert.AreEqual("FORD", invoice.Make);
            Assert.AreEqual("ESCAPE", invoice.Model);
            Assert.AreEqual("BLUE", invoice.Color);
        }

        [Test]
        public void GetInvoices_ByStockNumber()
        {
            var query = new InvoiceQuery();
            query.StockNumber = "BPB";

            var invoices = repository.GetInvoices(query);
            Assert.AreEqual(3, invoices.Count);
        }

        [Test]
        public void GetInvoices_ByReceiveDate()
        {
            var query = new InvoiceQuery();
            query.ReceivedDateStart = DateTime.Parse("12/7/2011");
            query.ReceivedDateEnd = DateTime.Parse("12/8/2011");

            var invoices = repository.GetInvoices(query);
            Assert.AreEqual(2, invoices.Count);
        }

        [Test]
        public void GetInvoices_ByAccount()
        {
            var query = new InvoiceQuery();
            query.AccountName = "DOBBS FORD AT WOLFCHASE";

            var invoices = repository.GetInvoices(query);
            Assert.AreEqual(4, invoices.Count);
        }

        [Test]
        public void GetInvoices_ByPartialAccount()
        {
            var query = new InvoiceQuery();
            query.AccountName = "DOBB";

            var invoices = repository.GetInvoices(query);
            Assert.AreEqual(17, invoices.Count);
        }

        [Test]
        public void GetInvoices_ByLocation()
        {
            var query = new InvoiceQuery();
            query.LocationId = 3;

            var invoices = repository.GetInvoices(query);
            Assert.AreEqual(5, invoices.Count);
        }

        [Test]
        public void GetInvoiceHistory_HasCompleteStockNumberHistory()
        {
            var history = repository.GetInvoiceHistory("CKB26781");
            Assert.AreEqual(3, history.Count);
            Assert.AreEqual(2, history.Select(h => h.InvoiceId).Distinct().Count());
        }

        [Test]
        public void SaveInvoice_MarkPaid()
        {
            var invoice = repository.GetInvoice(358900);
            Assert.IsFalse(invoice.IsPaid);
            invoice.IsPaid = true;
            repository.SaveInvoice(invoice);
            var invoice2 = repository.GetInvoice(358900);
            Assert.IsTrue(invoice2.IsPaid);
        }
    }
}
