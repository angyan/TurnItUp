using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Turnable.Tiled
{
    public class Tileset
    {
        public int FirstGlobalTileId { get; set; }
        public string Name { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int Columns { get; set; }

        public Tileset(XElement tileset)
        {
            FirstGlobalTileId = Convert.ToInt32(tileset.Attribute("firstgid").Value);
            Name = tileset.Attribute("name").Value;
            TileWidth = Convert.ToInt32(tileset.Attribute("tilewidth").Value);
            TileHeight = Convert.ToInt32(tileset.Attribute("tileheight").Value);
            Columns = Convert.ToInt32(tileset.Attribute("columns").Value);
        }
    }
}
