using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnable.Places;
using Turnable.Utilities;

namespace Turnable.Characters
{
    public class Player
    {
        public Position Position { get; set; }
        public Level Level { get; set; }

        public Player(Position position, Level level, uint tileId)
        {
            Level = level;
            Position = position;
            Level.Player = this;
            Level.TileMap.Layers["Characters"].SetTile(position, tileId);
        }
    }
}

