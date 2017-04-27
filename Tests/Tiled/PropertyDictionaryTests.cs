using System;
using NUnit.Framework;
using Turnable.Tiled;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Tests.Tiled
{
    [TestFixture]
    public class PropertyDictionaryTests
    {
        [Test]
        public void ParameterlessConstructor_Exists()
        {
            var properties = new PropertyDictionary();

            Assert.That(properties.Count, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_GivenACollectionOfXElements_InitializesAllProperties()
        {
            var propertyXElement = new XElement("property", new XAttribute("name", "enemyTint"), new XAttribute("type", "color"), new XAttribute("value", "#ffa33636"));
            var propertiesXElement = new XElement("properties", propertyXElement);

            var properties = new PropertyDictionary(propertiesXElement);

            Assert.That(properties.Count, Is.EqualTo(1));
        }
    }
}
