using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnable.Tiled;

namespace Turnable.Utilities
{
    public class TileMap
    {
        public Map Map { get; set; }

        public TileMap(string fullPath)
        {
            Map = Map.Load(fullPath);
        }
    }
}
