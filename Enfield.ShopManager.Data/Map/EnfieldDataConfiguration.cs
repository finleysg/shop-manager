using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Data.Map
{
    public class EnfieldDataConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "Enfield.ShopManager.Data.Graph" && 
                   type.BaseType != null &&
                   type.BaseType.Name.StartsWith("AutoMapBase");
        }
    }
}
