using System;
using Turnable.Utilities;

namespace Tests.Locations
{
    public class Viewport : IBounded
    {
        public Rectangle Bounds { get; set; }
        public TileMap TileMap { get; set; }
        public Position TileMapLocation { get; private set; }

        public Viewport()
        {
        }

        public Viewport(int width, int height) : this()
        {
            Bounds = new Rectangle(new Position(0, 0), width, height);
        }

        public Viewport(TileMap tileMap, int width, int height) : this(width, height)
        {
            TileMap = tileMap;
            TileMapLocation = Bounds.BottomLeft;
        }

        public Viewport(TileMap tileMap) : this(tileMap, tileMap.Map.Width, tileMap.Map.Height)
        {
        }

        public Viewport(TileMap tileMap, int width, int height, Position tileMapLocation) : this(tileMap, width, height)
        {
            Bounds.Move(tileMapLocation);
            TileMapLocation = Bounds.BottomLeft;
        }
    }
}