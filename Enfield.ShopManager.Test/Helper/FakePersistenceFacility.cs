using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enfield.ShopManager.Plumbing;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Castle.MicroKernel.Registration;

namespace Enfield.ShopManager.Tests
{
    // Lifestyle is changed from PerWebRequest to Transient.
    // ConnectionString is changed to the test bed.
    public class FakePersistenceFacility : PersistenceFacility
    {
        protected override void Init()
        {
            var config = this.BuildDatabaseConfiguration();

            Kernel.Register(
                Component.For<ISessionFactory>()
                    .UsingFactoryMethod(config.BuildSessionFactory),
                Component.For<ISession>()
                    .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifeStyle.Transient);
        }

        protected override IPersistenceConfigurer SetupDatabase()
        {
            return MsSqlConfiguration.MsSql2008
                .UseOuterJoin()
                .ConnectionString(x => x.Is("Data Source=(local);Initial Catalog=EnfieldMasterTestBed;User Id=shop; Password=password;Connection Timeout=10;Persist Security Info=True;"))
                .ShowSql();
        }
    }
}
