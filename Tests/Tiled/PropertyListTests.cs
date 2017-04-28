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

        [Test]
        public void Constructor_GivenAnXElementWithChildNodesForIndividualProperties_InitializesAllProperties()
        {
            var propertyXElement = new XElement("property", new XAttribute("name", "enemyTint"), new XAttribute("type", "color"), new XAttribute("value", "#ffa33636"));
            var propertiesXElement = new XElement("properties", propertyXElement);

            var properties = new PropertyList(propertiesXElement);

            Assert.That(properties.Count, Is.EqualTo(1));
            Property property = properties[0];
            Assert.That(property.Name, Is.EqualTo("enemyTint"));
            Assert.That(property.Value, Is.EqualTo("#ffa33636"));
            Assert.That(property.Type, Is.EqualTo(PropertyType.Color));
        }
    }
}
