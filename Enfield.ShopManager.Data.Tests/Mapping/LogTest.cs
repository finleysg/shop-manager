using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NUnit.Framework;
using FluentNHibernate.Testing;
using Enfield.ShopManager.Data.Graph;
using Enfield.ShopManager.Data.Tests.Fixtures;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    public class LogTest
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
        public void LoginAttemptLog_VerifyMap()
        {
            new PersistenceSpecification<LoginAttemptLog>(session, new EnfieldEqualityComparer())
                .CheckProperty(a => a.IpAddress, "1.2.3.4")
                .CheckProperty(a => a.LoginDate,  DateTime.Now)
                .CheckProperty(a => a.Reason, "Fail")
                .CheckProperty(a => a.ResultFlag, false)
                .CheckProperty(a => a.UserName, "Test")
                //.CheckReference(a => a.Location, mainShop)
                .VerifyTheMappings();
        }
    }
}
