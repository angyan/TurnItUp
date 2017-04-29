using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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
        public PropertyList Properties { get; set; }

        public static Map Load(string fullPath)
        {
            // REF: http://doc.mapeditor.org/reference/tmx-map-format/#map
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            var map = (Map)serializer.Deserialize(fileStream);

            //var map = new Map();
            //var tiledMapDocument = XDocument.Load(fullPath);

            //// Load properties for map
            //map.Properties = new PropertyDictionary(mapNode.Elements("properties").Single());

            return map;
        }
    }
}