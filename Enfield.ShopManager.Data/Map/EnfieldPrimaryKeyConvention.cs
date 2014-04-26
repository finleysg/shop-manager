using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Enfield.ShopManager.Data.Map
{
    public class EnfieldPrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
        }
    }
}
