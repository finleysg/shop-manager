using System;

namespace Enfield.ShopManager.Data.Map
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DoNotMapAttribute : Attribute
    {
    }
}
