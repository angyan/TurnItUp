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
            foreach(XElement property in properties.Descendants())
            {
                Console.WriteLine(property);
            }
        }
    }
}
