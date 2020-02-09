using System;
using Turnable.Places;
using Turnable.Utilities;

namespace Turnable.Places
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
            {
                int x, y;

                x = position.X - Bounds.Width / 2;
                y = position.Y - Bounds.Height / 2;

                // When parts of the viewport are out of bounds of the Map, move the new MapOrigin to compensate
                if ((Bounds.Width / 2 + position.X) > tileMapBounds.Width)
                {
                    x -= (Bounds.Width / 2 - (tileMapBounds.Width - position.X - 1));
                }
                if ((Bounds.Height / 2 + position.Y) > tileMapBounds.Height)
                {
                    y -= (Bounds.Height / 2 - (tileMapBounds.Height - position.Y - 1));
                }
                if (x < 0)
                {
                    x = 0;
                }
                if (y < 0)
                {
                    y = 0;
                }

                Bounds.MoveTo(new Position(x, y));
            }
        }
    }
}