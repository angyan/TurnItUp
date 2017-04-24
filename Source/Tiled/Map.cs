using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            // REF: http://doc.mapeditor.org/reference/tmx-map-format/#map
            return new Map();
        }
    }
}
