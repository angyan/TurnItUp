using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    [XmlType("property")]
    public class Property
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
        [XmlAttribute("type")]
        public PropertyType Type { get; set; }

        public Property()
        {
        }
    }
}
