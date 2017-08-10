using System;

namespace Turnable.Utilities
{
    public class Rectangle
    {
        public Position BottomLeft { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle(Position corner1, Position corner2)
        {
            // TODO: Throw exception if corners are not opposite corners
            // No matter which two opposite corners are passed in, the constructor should work correctly.
            BottomLeft = new Position(Math.Min(corner1.X, corner2.X), Math.Min(corner1.Y, corner2.Y));
            var topRight = new Position(Math.Max(corner1.X, corner2.X), Math.Max(corner1.Y, corner2.Y));
            Width = topRight.X - BottomLeft.X + 1;
            Height = topRight.Y - BottomLeft.Y + 1;
        }

        public Rectangle(Position bottomLeft, int width, int height) : this(bottomLeft, new Position(bottomLeft.X + width - 1, bottomLeft.Y + height - 1))
        {
        }

        public Position TopRight
        {
            get
            {
                return new Position(BottomLeft.X + Width - 1, BottomLeft.Y + Height - 1);
            }
        }

        public bool Contains(Position position)
        {
            return (position.X >= BottomLeft.X && position.X <= TopRight.X && position.Y >= BottomLeft.Y && position.Y <= TopRight.Y);
        }

        public bool Contains(Rectangle rectangle)
        {
            return (Contains(rectangle.BottomLeft) && Contains(rectangle.TopRight));
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    yield return new Position(BottomLeft.X + col, BottomLeft.Y + row);
                }
            }
        }

        public void MoveTo(Position newPosition)
        {
            BottomLeft = newPosition;
        }

        public override string ToString()
        {
            return String.Format("Rectangle {{BottomLeft: {0}, TopRight: {1}}}", BottomLeft, TopRight);
        }
    }
}
