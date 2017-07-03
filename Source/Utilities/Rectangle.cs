using System;

namespace Turnable.Utilities
{
    public class Rectangle
    {
        public Position BottomLeft { get; set; }
        public Position TopRight { get; set; }

        public Rectangle(Position corner1, Position corner2)
        {
            // TODO: Throw exception if corners are not opposite corners
            // No matter which two opposite corners are passed in, the constructor should work correctly.
            BottomLeft = new Position(Math.Min(corner1.X, corner2.X), Math.Min(corner1.Y, corner2.Y));
            TopRight = new Position(Math.Max(corner1.X, corner2.X), Math.Max(corner1.Y, corner2.Y));
        }

        public Rectangle(Position bottomLeft, int width, int height) : this(bottomLeft, new Position(bottomLeft.X + width - 1, bottomLeft.Y + height - 1))
        {
        }

        public int Width {
            get
            {
                return TopRight.X - BottomLeft.X + 1;
            }
        }

        public int Height
        {
            get
            {
                return TopRight.Y - BottomLeft.Y + 1;
            }
        }

        public bool Contains(Position position)
        {
            return (position.X >= BottomLeft.X && position.X <= TopRight.X && position.Y >= BottomLeft.Y && position.Y <= TopRight.Y);
        }
    }
}
