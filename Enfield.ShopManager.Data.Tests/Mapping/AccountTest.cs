using System;
using System.Linq;
using Enfield.ShopManager.Data.Graph;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using Enfield.ShopManager.Data.Tests.Fixtures;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    public class AccountTest
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
        public void Account_WithContacts_VerifyMap()
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
                new Contact() {
                    ContactDetail = "john@dough.com",
                    DoNotify = true,
                    FirstName = "John",
                    LastName = "Dough",
                    ContactType = email,
                });
            account.AddContact(
                new Contact() {
                    ContactDetail = "123-456-7890",
                    DoNotify = false,
                    FirstName = "Jane",
                    LastName = "Dough",
                    ContactType = phone,
                });

            session.SaveOrUpdate(account);
            session.Flush();

            var account2 = session.QueryOver<Account>().Where(a => a.Id == account.Id).SingleOrDefault();
            Assert.IsNotNull(account2);
            Assert.AreEqual(2, account2.ContactList.Count);
            Assert.IsNotNull(account2.ContactList.Where(c => c.ContactDetail == "john@dough.com").FirstOrDefault());
        }

        [Test]
        public void Account_NoContacts_VerifyMap()
        {
            var dealer = session.Get<AccountType>(1);

            new PersistenceSpecification<Account>(session, new EnfieldEqualityComparer())
                .CheckProperty(a => a.Name, "FAKE ACCOUNT")
                .CheckProperty(a => a.ModifyDate, DateTime.Now)
                .CheckProperty(a => a.ModifyUser, "TEST")
                .CheckReference(a => a.AccountType, dealer)
                .VerifyTheMappings();
        }

        [Test]
        public void Contact_VerifyMap()
        {
            var account = session.Get<Account>(699);
            var contactType = session.Get<ContactType>(1);

            new PersistenceSpecification<Contact>(session, new EnfieldEqualityComparer())
                .CheckProperty(c => c.ContactDetail, "123.456.7890")
                .CheckProperty(c => c.DoNotify, false)
                .CheckProperty(c => c.FirstName, "Bob")
                .CheckProperty(c => c.LastName, "Burger")
                .CheckReference(c => c.Account, account)
                .CheckReference(c => c.ContactType, contactType)
                .VerifyTheMappings();
        }
    }
}
