using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Turnable.Tiled
{
    public class Map
    {
        public string Version { get; set; }
        public Orientation Orientation { get; set; }
        public RenderOrder RenderOrder { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int NextObjectId { get; set; }

        public static Map Load(string fullPath)
        {
            var map = new Map();
            var tiledMapDocument = XDocument.Load(fullPath);

            // REF: http://doc.mapeditor.org/reference/tmx-map-format/#map
            var mapNode = tiledMapDocument.Elements("map").Single();

            map.Version = mapNode.Attribute("version").Value;
            map.Width = Convert.ToInt32(mapNode.Attribute("width").Value);
            map.Height = Convert.ToInt32(mapNode.Attribute("height").Value);
            map.TileWidth = Convert.ToInt32(mapNode.Attribute("tilewidth").Value);
            map.TileHeight = Convert.ToInt32(mapNode.Attribute("tileheight").Value);
            map.NextObjectId = Convert.ToInt32(mapNode.Attribute("nextobjectid").Value);

            return map;
        }
    }
}
