using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace Enfield.ShopManager.Data.Map
{
    public class EnfieldForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";

            if (property.DeclaringType.Name == "StockNumberHistory")
                return "StockNumber";   

            return property.Name + "Id";
        }
    }
}
