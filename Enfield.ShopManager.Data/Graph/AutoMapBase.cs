using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Graph
{
    public abstract class AutoMapBase<TGraph> where TGraph : AutoMapBase<TGraph>
    {
        private int? oldHashCode;
        public virtual int Id { get; protected set; }

        public virtual bool Equals(AutoMapBase<TGraph> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it
            if (oldHashCode.HasValue)
                return oldHashCode.Value;

            var thisIsTransient = Equals(Id, 0);

            // When this instance is transient, we use the base GetHashCode()
            // and remember it, so an instance can NEVER change its hash code.
            if (thisIsTransient)
            {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }
            return (base.GetHashCode() * 31) + Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as TGraph;
            if (other == null)
                return false;

            // handle the case of comparing two NEW objects
            var otherIsTransient = Equals(other.Id, 0);
            var thisIsTransient = Equals(Id, 0);
            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.Id.Equals(Id);
        }
    }
}
