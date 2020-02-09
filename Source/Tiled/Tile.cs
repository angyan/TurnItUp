using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    [XmlType("tile")]
    public class Tile
    {
        [XmlAttribute("id")]
        public uint Id{ get; set; }
        [XmlArray(ElementName = "properties")]
        [XmlArrayItem(ElementName = "property")]
        public List<Property> Properties { get; set; }

        public Tile()
        {
        }
    }
}
