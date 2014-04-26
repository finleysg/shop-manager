using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enfield.ShopManager.Security;

namespace Enfield.ShopManager.Tests.Security
{
    public class TokenTests
    {
        private Token token;

        [SetUp]
        public void TokenSetup()
        {
            token = new Token()
            {
                CreateDate = DateTime.Now,
                IpAddress = "123.45.678.90",
                LocationId = 2,
                UserId = 55,
                Role = (int)RolesEnum.Employee
            };
            TokenHasher.Hash(token);
        }

        [TearDown]
        public void TokenTeardown()
        {
            token = null;
        }

        [Test]
        public void TokenHasher_NoTamper_IsValid()
        {
            Assert.IsTrue(TokenHasher.IsValid(token));
        }

        [Test]
        public void TokenHasher_TamperWithCreateDate_IsNotValid()
        {
            token.CreateDate = token.CreateDate.AddSeconds(1);
            Assert.IsFalse(TokenHasher.IsValid(token));
        }

        [Test]
        public void TokenHasher_TamperWithIp_IsNotValid()
        {
            token.IpAddress = "123.45.678.9";
            Assert.IsFalse(TokenHasher.IsValid(token));
        }

        [Test]
        public void TokenHasher_TamperWithRole_IsNotValid()
        {
            token.Role = (int)RolesEnum.Administrator;
            Assert.IsFalse(TokenHasher.IsValid(token));
        }

        [Test]
        public void TokenSerializer_BeforeEqualsAfter()
        {
            var serialized = TokenSerializer.Serialize(token);
            var deserialized = TokenSerializer.Deserialize(serialized);

            //must reapply the IP - comes from the request header, not saved
            deserialized.IpAddress = "123.45.678.90";

            Assert.AreNotSame(token, deserialized);
            Assert.AreEqual(token.CreateDate, deserialized.CreateDate);
            Assert.AreEqual(token.Role, deserialized.Role);
            Assert.AreEqual(token.IpAddress, deserialized.IpAddress);
            Assert.AreEqual(token.LocationId, deserialized.LocationId);
            Assert.AreEqual(token.UserId, deserialized.UserId);
        }

        [Test]
        public void TokenSerializer_HashStillValid()
        {
            var serialized = TokenSerializer.Serialize(token);
            var deserialized = TokenSerializer.Deserialize(serialized);

            //must reapply the IP - comes from the request header, not saved
            deserialized.IpAddress = "123.45.678.90";

            Assert.AreNotSame(token, deserialized);
            Assert.IsTrue(TokenHasher.IsValid(deserialized));
        }

        [Test]
        public void TokenSerializer_DifferentIp_NotValid()
        {
            var serialized = TokenSerializer.Serialize(token);
            var deserialized = TokenSerializer.Deserialize(serialized);

            //must reapply the IP - comes from the request header, not saved
            deserialized.IpAddress = "213.54.678.90";

            Assert.AreNotSame(token, deserialized);
            Assert.IsFalse(TokenHasher.IsValid(deserialized));
        }
    }
}
