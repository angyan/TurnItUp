using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    [XmlType("tileset")]
    public class Tileset
    {
        [XmlAttribute("firstgid")]
        public int FirstGlobalTileId { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }
        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }
        [XmlAttribute("columns")]
        public int Columns { get; set; }
    }
}
