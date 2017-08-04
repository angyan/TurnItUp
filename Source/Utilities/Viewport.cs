using System;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Places
{
    public class Viewport : IBounded
    {
        public Rectangle Bounds { get; set; }
        public Position Position { get; private set; }

        public Viewport()
        {
        }

        public Viewport(int width, int height) : this()
        {
            Bounds = new Rectangle(new Position(0, 0), width, height);
            Position = Bounds.BottomLeft;
        }

        public Viewport(int width, int height, Position position) : this(width, height)
        {
            Bounds.Move(position);
            Position = Bounds.BottomLeft;
        }

        public void Focus(Position position)
        {
            Position newPosition = new Position(position.X - Bounds.Width / 2, position.Y - Bounds.Height / 2);

            Bounds.Move(newPosition);
            Position = Bounds.BottomLeft;
        }
    }
}