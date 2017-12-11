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
            Position currentViewportPosition = Bounds.BottomLeft.Clone();

            Bounds.MoveTo(Bounds.BottomLeft.NeighboringPosition(direction));

            // The viewport should be able to move in a certain direction as much as possible while still staying within bounds.
            // Example: The left edge of a Viewport is flush against the left edge of the Map. Trying to move the Viewport NW should still move the Viewport North.

            // If the viewport's bound's X value is invalid, reset it
            if (Bounds.BottomLeft.X < 0 || (Bounds.BottomLeft.X + Bounds.Width) > tileMapBounds.Width)
            {
                Bounds.MoveTo(new Position(currentViewportPosition.X, Bounds.BottomLeft.Y));
            }

            // If the viewport's bound's Y value is invalid, reset it
            if (Bounds.BottomLeft.Y < 0 || (Bounds.BottomLeft.Y + Bounds.Height) > tileMapBounds.Height)
            {
                Bounds.MoveTo(new Position(Bounds.BottomLeft.X, currentViewportPosition.Y));
            }
        }

        public void FocusOn(Position position, Rectangle tileMapBounds)
        {
            Position newPosition = new Position(position.X - Bounds.Width / 2, position.Y - Bounds.Height / 2);

            // If the viewport's bound's X value is invalid, set it to a valid value
            if (newPosition.X < 0)
            {
                newPosition.X = 0;
            }

            // If the viewport's bound's Y value is invalid, set it to a valid value
            if (newPosition.Y < 0)
            {
                newPosition.Y = 0;
            }

            Bounds.MoveTo(newPosition);
        }
    }
}