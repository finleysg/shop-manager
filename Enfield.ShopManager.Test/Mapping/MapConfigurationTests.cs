using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enfield.ShopManager.Mapping;
using AutoMapper;

namespace Enfield.ShopManager.Tests.Mapping
{
    public class MapConfigurationTests
    {
        [Test]
        public void MapConfiguration_MappingIsValid()
        {
            MapConfiguration config = new MapConfiguration();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
