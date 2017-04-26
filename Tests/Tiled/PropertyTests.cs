using System;
using System.IO;
using NUnit.Framework;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class PropertyTests
    {
        [Test]
        public void Constructor_GivenANameAndValue_InitializesTheProperty()
        {
            var property = new Property("Name", "Value");

            Assert.AreEqual("Name", property.Name);
            Assert.AreEqual("Value", property.Value);
            Assert.AreEqual(PropertyType.String, property.Type);
        }

        [Test]
        public void Constructor_GivenANameValueAndType_InitializesTheProperty()
        {
            var property = new Property("Name", "Value", PropertyType.Color);

            Assert.AreEqual("Name", property.Name);
            Assert.AreEqual("Value", property.Value);
            Assert.AreEqual(PropertyType.Color, property.Type);
        }
    }
}
