using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enfield.ShopManager.Data.Graph;
using NUnit.Framework;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    public class EqualityTests
    {
        [Test]
        public void Equals_WithTwoNullObjects_ReturnsTrue()
        {
            const SimpleDomainObject obj1 = null;
            const SimpleDomainObject obj2 = null;

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(true, equality);
        }

        [Test]
        public void Equals_WithNullObject_ReturnsFalse()
        {
            const SimpleDomainObject obj1 = null;
            var obj2 = new SimpleDomainObject();

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(false, equality);
        }

        [Test]
        public void Equals_WithTransientObjects_ReturnsFalse()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new SimpleDomainObject();

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(false, equality);
        }

        [Test]
        public void Equals_WithOneTransientObject_ReturnsFalse()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new SimpleDomainObject();

            obj1.SetId(1);

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(false, equality);
        }

        [Test]
        public void Equals_WithDifferentIds_ReturnsFalse()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new SimpleDomainObject();

            obj1.SetId(1);
            obj2.SetId(2);

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(false, equality);
        }

        [Test]
        public void Equals_WithSameIds_ReturnsTrue()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new SimpleDomainObject();

            obj1.SetId(1);
            obj2.SetId(1);

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(true, equality);
        }

        [Test]
        public void Equals_WithSameIdsInSubclass_ReturnsTrue()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new SubSimpleDomainObject();

            obj1.SetId(1);
            obj2.SetId(1);

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(true, equality);
        }

        [Test]
        public void Equals_WithDifferentIdsInDisparateClasses_ReturnsFalse()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new OtherSimpleDomainObject();

            obj1.SetId(1);
            obj2.SetId(2);

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(false, equality);
        }

        [Test]
        public void Equals_WithSameIdsInDisparateClasses_ReturnsFalse()
        {
            var obj1 = new SimpleDomainObject();
            var obj2 = new OtherSimpleDomainObject();

            obj1.SetId(1);
            obj2.SetId(1);

            var equality = Equals(obj1, obj2);

            Assert.AreEqual(false, equality);
        }

    }

    public class SimpleDomainObject : AutoMapBase<SimpleDomainObject>
    {
        public void SetId(int id)
        {
            Id = id;
        }
    }

    public class SubSimpleDomainObject : SimpleDomainObject { }

    public class OtherSimpleDomainObject : AutoMapBase<OtherSimpleDomainObject>
    {
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
