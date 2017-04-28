using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    public class Property
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Value")]
        public string Value { get; set; }
        [XmlAttribute("Type")]
        public PropertyType Type { get; set; }

        public Property()
        {
        }

        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Property(string name, string value, PropertyType type) : this(name, value)
        {
            Type = type;
        }

        public Property(XElement property) : this(property.Attribute("name").Value, property.Attribute("value").Value, (PropertyType)Enum.Parse(typeof(PropertyType), property.Attribute("type").Value, true))
        {
        }
    }
}
