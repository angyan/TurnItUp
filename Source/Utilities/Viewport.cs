using System;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Places
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

        public Viewport(int width, int height, Position position) : this(width, height)
        {
            Bounds.MoveTo(position);
        }

        public void MoveTo(Position newPosition)
        {
            Bounds.MoveTo(newPosition);
        }

        public void MoveInDirection(Direction direction, Rectangle tileMapBounds)
        {
            Bounds.MoveTo(Bounds.BottomLeft.NeighboringPosition(direction));
        }

        public void FocusOn(Position position)
        {
            Position newPosition = new Position(position.X - Bounds.Width / 2, position.Y - Bounds.Height / 2);

            Bounds.MoveTo(newPosition);
        }
    }
}