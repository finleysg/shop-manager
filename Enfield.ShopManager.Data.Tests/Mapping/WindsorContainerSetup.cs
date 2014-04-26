using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Castle.Windsor;
using FluentNHibernate.Testing;
using Enfield.ShopManager.Data.Graph;
using NHibernate;
using System.Collections;
using Enfield.ShopManager.Data.Tests.Fixtures;

namespace Enfield.ShopManager.Data.Tests.Mapping
{
    [SetUpFixture]
    public class WindsorContainerSetup
    {

        [SetUp]
        public void ControllerInstallerSetup()
        {
            WindsorPersistenceFixture.InitializeContainer();
        }

        [TearDown]
        public void ControllerInstallerTeardown()
        {
            WindsorPersistenceFixture.DisposeContainer();
        }
    }
}
