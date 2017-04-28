using System;
using System.IO;
using NUnit.Framework;
using Turnable.Tiled;
using System.Xml.Linq;

namespace Tests.Tiled
{
    [TestFixture]
    public class PropertyTests
    {
        [Test]
        public void ParameterlessConstructor_Exists()
        {
            var property = new Property();

            Assert.That(property.Name, Is.Null);
            Assert.That(property.Value, Is.Null);
            Assert.That(property.Type, Is.EqualTo(PropertyType.String));
        }

        [Test]
        public void Constructor_GivenANameAndValue_InitializesTheProperty()
        {
            var property = new Property("Name", "Value");

            Assert.That(property.Name, Is.EqualTo("Name"));
            Assert.That(property.Value, Is.EqualTo("Value"));
            Assert.That(property.Type, Is.EqualTo(PropertyType.String));
        }

        [Test]
        public void Constructor_GivenANameValueAndType_InitializesTheProperty()
        {
            var property = new Property("Name", "Value", PropertyType.Color);

            Assert.That(property.Name, Is.EqualTo("Name"));
            Assert.That(property.Value, Is.EqualTo("Value"));
            Assert.That(property.Type, Is.EqualTo(PropertyType.Color));
        }

        [Test]
        public void Constructor_GivenAPropertyXElement_InitializesTheProperty()
        {
            var propertyXElement = new XElement("property", new XAttribute("name", "enemyTint"), new XAttribute("type", "color"), new XAttribute("value", "#ffa33636"));

            var property = new Property(propertyXElement);

            Assert.That(property.Name, Is.EqualTo("enemyTint"));
            Assert.That(property.Value, Is.EqualTo("#ffa33636"));
            Assert.That(property.Type, Is.EqualTo(PropertyType.Color));
        }

        [Test]
        public void Constructor_GivenAPropertyXElement_IgnoresCaseOfTypeWhenInitializingTheProperty()
        {
            var propertyXElement = new XElement("property", new XAttribute("name", "enemyTint"), new XAttribute("type", "FILE"), new XAttribute("value", "#ffa33636"));

            var property = new Property(propertyXElement);

            Assert.That(property.Type, Is.EqualTo(PropertyType.File));
        }
    }
}
