using System;
using System.Xml;

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
            var tiledMapDocument = new XmlDocument();
            tiledMapDocument.Load(fullPath);

            // REF: http://doc.mapeditor.org/reference/tmx-map-format/#map
            var mapNode = tiledMapDocument.DocumentElement.SelectSingleNode("/map");
            Console.WriteLine(mapNode.Attributes);
            map.Version = mapNode.Attributes["version"].Value;
            map.Width = (int)(mapNode.Attributes["width"].Value);
            map.Version = mapNode.Attributes["version"].Value;

            return map;
        }
    }
}
