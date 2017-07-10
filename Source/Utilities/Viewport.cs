using System;
using Turnable.Utilities;

namespace Tests.Locations
{
    public class Viewport : IBounded
    {
        public Rectangle Bounds { get; set; }

        public Viewport()
        {
        }

        public Viewport(int width, int height) : this()
        {
            Bounds = new Rectangle(new Position(0, 0), width, height);
        }
    }
}