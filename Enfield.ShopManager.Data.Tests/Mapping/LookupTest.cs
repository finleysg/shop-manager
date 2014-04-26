using Enfield.ShopManager.Data.Graph;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using System;
using System.Linq;
using Enfield.ShopManager.Data.Tests.Fixtures;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    public class LookupTest
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
        public void AccountType_VerifyMap()
        {
            new PersistenceSpecification<AccountType>(session, new EnfieldEqualityComparer())
                .CheckProperty(a => a.Description, "GOLD")
                .CheckProperty(a => a.IsActive, true)
                .CheckProperty(a => a.ModifyDate, DateTime.Now)
                .CheckProperty(a => a.ModifyUser, "TEST")
                .CheckProperty(a => a.TaxRate, 6.5)
                .VerifyTheMappings();
        }

        [Test]
        public void AccountType_LoadDealer_HasAllChildren()
        {
            var dealer = session.Get<AccountType>(1);
            Assert.IsNotNull(dealer);
            Assert.IsTrue(dealer.ServiceTypeList.Count > 1);
            Assert.IsTrue(dealer.ServiceTypeList[0].LaborTypeList.Count > 1);
        }

        [Test]
        public void AccountType_CanAddService()
        {
            var serviceType = session.Get<ServiceType>(29); //PIN STRIPE
            var privates = session.Get<AccountType>(2);
            privates.AddService(
                new AccountTypeService()
                {
                    IsActive = true,
                    ServiceType = serviceType
                });

            session.SaveOrUpdate(privates);
            session.Flush();

            var privates2 = session.QueryOver<AccountType>().Where(a => a.Id == 2).SingleOrDefault();
            Assert.IsNotNull(privates2);
            Assert.IsNotNull(privates2.ServiceTypeList.Where(c => c.ServiceType.Description == "PIN STRIPE").FirstOrDefault());
        }

        [Test]
        public void AccountTypeService_VerifyMap()
        {
            var serviceType = session.Get<ServiceType>(29); //PIN STRIPE
            var privates = session.Get<AccountType>(2);

            new PersistenceSpecification<AccountTypeService>(session, new EnfieldEqualityComparer())
                .CheckProperty(s => s.IsActive, true)
                .CheckReference(s => s.ServiceType, serviceType)
                .CheckReference(s => s.AccountType, privates)
                .VerifyTheMappings();
        }

        [Test]
        public void LaborType_VerifyMap()
        {
            var service = session.Get<AccountTypeService>(1);
            var labor = session.Get<LaborType>(40);

            new PersistenceSpecification<AccountTypeLabor>(session, new EnfieldEqualityComparer())
                .CheckProperty(l => l.DefaultRateType, "X")
                .CheckReference(l => l.AccountTypeService, service)
                .CheckReference(l => l.LaborType, labor)
                .VerifyTheMappings();
        }

        [Test]
        public void AccountTypeService_CanAddLabor()
        {
            var service = session.Get<AccountTypeService>(1);
            var labor = session.Get<LaborType>(40);

            service.AddLabor(
                new AccountTypeLabor()
                {
                    DefaultRateType = "X",
                    LaborType = labor
                });

            session.SaveOrUpdate(service);
            session.Flush();

            var service2 = session.QueryOver<AccountTypeService>().Where(a => a.Id == 1).SingleOrDefault();
            Assert.IsNotNull(service2);
            Assert.IsNotNull(service2.LaborTypeList.Where(l => l.LaborType.Id == 40).FirstOrDefault());
        }

        [Test]
        public void ContactType_VerifyMap()
        {
            new PersistenceSpecification<ContactType>(session)
                .CheckProperty(c => c.Description, "SMOKE SIGNALS")
                .VerifyTheMappings();
        }

        [Test]
        public void AccountLaborType_VerifyMap()
        {
            new PersistenceSpecification<LaborType>(session)
                .CheckProperty(c => c.Description, "MOP")
                .VerifyTheMappings();
        }

        [Test]
        public void ServiceType_VerifyMap()
        {
            new PersistenceSpecification<ServiceType>(session)
                .CheckProperty(c => c.Description, "REMOVE DOORS")
                .VerifyTheMappings();
        }

        [Test]
        public void Location_VerifyMap()
        {
            new PersistenceSpecification<Location>(session)
                .CheckProperty(c => c.Name, "PLYMOUTH")
                .VerifyTheMappings();
        }

    }
}
