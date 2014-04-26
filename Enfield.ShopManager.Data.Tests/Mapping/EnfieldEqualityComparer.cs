using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    public class EnfieldEqualityComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is DateTime && y is DateTime)
            {
                return ((DateTime)x - (DateTime)y).Seconds <= 1;
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
