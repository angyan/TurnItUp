using System;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Locations
{
    public class Viewport : IBounded
    {
        public Rectangle Bounds { get; set; }
        public Level Level { get; set; }
        public Position Location { get; private set; }

        public Viewport()
        {
        }

        public Viewport(int width, int height) : this()
        {
            Bounds = new Rectangle(new Position(0, 0), width, height);
        }

        public Viewport(Level level, int width, int height) : this(width, height)
        {
            Level = level;
            TileMapLocation = Bounds.BottomLeft;
        }

        public Viewport(Level level) : this(level, level.TileMap.Map.Width, level.TileMap.Map.Height)
        {
        }

        public Viewport(Level level, int width, int height, Position location) : this(level, width, height)
        {
            Bounds.Move(location);
            TileMapLocation = Bounds.BottomLeft;
        }
    }
}