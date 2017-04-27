using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Turnable.Tiled
{
    public class PropertyDictionary : Dictionary<string, Property>
    {
        public PropertyDictionary()
        {
        }

        public PropertyDictionary(XElement properties)
        {
            Console.WriteLine(properties);
            Console.WriteLine(properties.Descendants().Count<XElement>());
            foreach (XElement property in properties.Descendants())
            {
                string name = property.Attribute("name").Value;
                string value = property.Attribute("value").Value;
                PropertyType propertyType = (PropertyType)Enum.Parse(typeof(PropertyType), property.Attribute("type").Value, true);

                this[name] = new Property(name, value, propertyType);
            }
        }
    }
}
