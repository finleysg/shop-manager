using Castle.Core.Internal;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Enfield.ShopManager.Data.Audit;
using Enfield.ShopManager.Data.Graph;
using Enfield.ShopManager.Data.Map;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;

namespace Enfield.ShopManager.Plumbing
{
    public class PersistenceFacility : AbstractFacility
    {
        protected override void Init()
        {
            var config = BuildDatabaseConfiguration();

            Kernel.Register(
                Component.For<ISessionFactory>()
                    .UsingFactoryMethod(config.BuildSessionFactory),
                Component.For<ISession>() //.Interceptors(InterceptorReference.ForType<SessionInterceptor>()
                    .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifeStyle.PerWebRequest);
        }

        protected virtual Configuration BuildDatabaseConfiguration()
        {
            return Fluently.Configure()
                .Database(SetupDatabase)
                .ProxyFactoryFactory(typeof(ProxyFactoryFactory))
                .Mappings(m => {
                    m.AutoMappings.Add(CreateMappingModel()); //.ExportTo(@".\");
                    m.FluentMappings.AddFromAssemblyOf<EnfieldDataConfiguration>();
                 })
                .ExposeConfiguration(ConfigurePersistence)
                .BuildConfiguration();
        }

        protected virtual AutoPersistenceModel CreateMappingModel()
        {
            var m = AutoMap.AssemblyOf<Invoice>(new EnfieldDataConfiguration())
                .Conventions.Setup(c =>
                    {
                        c.Add<EnfieldPrimaryKeyConvention>();
                        c.Add<EnfieldForeignKeyConvention>();
                        c.Add<EnfieldHasManyConvention>();
                        //c.Add<EnfieldReferenceConvention>();
                    })
                .OverrideAll(ShouldIgnoreProperty)
                .Override<Location>(o => { o.Map(l => l.Name, "LocationName"); })
                .Override<Account>(o => { o.Map(a => a.Name, "AccountName"); })
                .IgnoreBase(typeof(AutoMapBase<>));
                    
            return m;
        }

        protected virtual IPersistenceConfigurer SetupDatabase()
        {
            return MsSqlConfiguration.MsSql2008
                .UseOuterJoin()
                .ConnectionString(x => x.FromConnectionStringWithKey("ApplicationServices"))
                .ShowSql();
        }

        protected virtual void ConfigurePersistence(Configuration config)
        {
            config.AppendListeners(ListenerType.PreUpdate, new[] { new AuditEventListener() });
            config.AppendListeners(ListenerType.PreInsert, new[] { new AuditEventListener() });
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
        }

        private void ShouldIgnoreProperty(IPropertyIgnorer property)
        {
            property.IgnoreProperties(p => p.MemberInfo.HasAttribute<DoNotMapAttribute>());
        }
    }
}