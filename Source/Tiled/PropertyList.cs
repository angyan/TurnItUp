using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    [XmlElement("properties")]
    public class PropertyList : List<Property>
    {
        public PropertyList()
        {
        }

        public PropertyList(XElement properties)
        {
            foreach (XElement property in properties.Descendants())
            {
                Add(new Property(property));
            }
        }
    }
}
