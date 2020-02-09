using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    [XmlRoot("map")]
    public class Map
    {
        [XmlAttribute("version")]
        public string Version { get; set; }
        public Orientation Orientation { get; set; }
        public RenderOrder RenderOrder { get; set; }
        [XmlAttribute("width")]
        public int Width { get; set; }
        [XmlAttribute("height")]
        public int Height { get; set; }
        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }
        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }
        [XmlAttribute("nextobjectid")]
        public int NextObjectId { get; set; }
        [XmlArray(ElementName = "properties")]
        [XmlArrayItem(ElementName = "property")]
        public List<Property> Properties { get; set; }
        [XmlElement("tileset")]
        public List<Tileset> Tilesets { get; set; }
        [XmlElement("layer")]
        public List<Layer> Layers { get; set; }

        public static Map Load(string fullPath)
        {
            // REF: http://doc.mapeditor.org/reference/tmx-map-format/#map
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            var map = (Map)serializer.Deserialize(fileStream);

            fileStream.Close();
            return map;
        }
    }
}