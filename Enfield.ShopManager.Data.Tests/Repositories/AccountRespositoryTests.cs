using System;
using System.Collections.Generic;
using System.Linq;
using Enfield.ShopManager.Data.Graph;
using Enfield.ShopManager.Data.Repository;
using Enfield.ShopManager.Data.Tests.Fixtures;
using NHibernate;
using NUnit.Framework;

namespace Enfield.ShopManager.Data.Tests.Repositories
{
	public class AccountRespositoryTests
	{
        ISession session;
        ITransaction tx;
        AccountRepository repository;

        [SetUp]
        public void Setup()
        {
            session = WindsorPersistenceFixture.Container.Resolve<ISession>();
            repository = new AccountRepository();
            repository.Session = session;
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
        public void GetAccount_ReturnsBillGuy()
        {
            Account billGuy = repository.GetAccount(699);
            Assert.IsNotNull(billGuy);
            Assert.AreEqual(1, billGuy.AccountType.Id);
            Assert.AreEqual("BILL GUY", billGuy.Name);
        }

        [Test]
        public void GetAccounts_All_Returns3()
        {
            IList<Account> all = repository.GetAccounts();
            Assert.IsNotEmpty(all.ToArray());
            Assert.AreEqual(3, all.Count);
        }

        [Test]
        public void GetAccounts_WithFilter_Returns2()
        {
            IList<Account> all = repository.GetAccounts("DOBB");
            Assert.IsNotEmpty(all.ToArray());
            Assert.AreEqual(2, all.Count);
        }

        [Test]
        public void SaveAccount_NoContacts()
        {
            var dealer = session.Get<AccountType>(1);

            var account = new Account()
            {
                Name = "Acme Enterprises",
                AccountType = dealer,
                ModifyDate = DateTime.Now,
                ModifyUser = "test"
            };

            repository.SaveAccount(account);

            var account2 = repository.GetAccount(account.Id);
            Assert.IsNotNull(account2);
            Assert.IsTrue(account2.Id > 0);
            Assert.IsEmpty(account2.ContactList.ToArray());
            Assert.AreEqual(0, account2.ContactList.Count);
        }

        [Test]
        public void SaveAccount_WithContacts()
        {
            var dealer = session.Get<AccountType>(1);
            var phone = session.Get<ContactType>(1);
            var email = session.Get<ContactType>(5);

            var account = new Account()
            {
                Name = "Acme Enterprises",
                AccountType = dealer,
                ModifyDate = DateTime.Now,
                ModifyUser = "test"
            };

            account.AddContact(
                new Contact()
                {
                    ContactDetail = "john@dough.com",
                    DoNotify = true,
                    FirstName = "John",
                    LastName = "Dough",
                    ContactType = email,
                });

            repository.SaveAccount(account);

            var account2 = repository.GetAccount(account.Id);
            Assert.IsNotNull(account2);
            Assert.AreEqual(1, account2.ContactList.Count);
            Assert.IsNotNull(account2.ContactList.Where(c => c.ContactDetail == "john@dough.com").FirstOrDefault());
        }
    }
}
