using System;
using NUnit.Framework;
using Turnable.Tiled;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Tiled
{
    [TestFixture]
    public class PropertyListTests
    {
        [Test]
        public void ParameterlessConstructor_Exists()
        {
            var properties = new PropertyList();

            Assert.That(properties.Count, Is.EqualTo(0));
        }
    }
}
