using System;
using NUnit.Framework;
using Turnable.Utilities;
using System.Collections.Generic;

namespace Tests.Utilities
{
    [TestFixture]
    public class RectangleTests
    {
        [Test]
        public void Constructor_GivenTheBottomLeftCornerWidthAndHeight_InitializesTheRectangle()
        {
            Position bottomLeft = new Position(0, 0);
            Rectangle rectangle = new Rectangle(bottomLeft, 5, 4);

            Assert.That(rectangle.BottomLeft, Is.EqualTo(new Position(0, 0)));
            Assert.That(rectangle.TopRight, Is.EqualTo(new Position(4, 3)));
            Assert.That(rectangle.Width, Is.EqualTo(5));
            Assert.That(rectangle.Height, Is.EqualTo(4));
        }

        [Test]
        public void Constructor_GivenAnyTwoCorners_InitializesTheRectangle()
        {
            Position topLeft = new Position(1, 4);
            Position bottomRight = new Position(3, 1);
            Position topRight = new Position(3, 4);
            Position bottomLeft = new Position(1, 1);

            // Bottom left, top right corners
            Rectangle rectangle = new Rectangle(bottomLeft, topRight);

            Assert.That(rectangle.BottomLeft, Is.EqualTo(bottomLeft));
            Assert.That(rectangle.TopRight, Is.EqualTo(topRight));
            Assert.That(rectangle.Width, Is.EqualTo(3));
            Assert.That(rectangle.Height, Is.EqualTo(4));

            // Bottom right, top left corners
            rectangle = new Rectangle(bottomRight, topLeft);

            Assert.That(rectangle.BottomLeft, Is.EqualTo(bottomLeft));
            Assert.That(rectangle.TopRight, Is.EqualTo(topRight));
            Assert.That(rectangle.Width, Is.EqualTo(3));
            Assert.That(rectangle.Height, Is.EqualTo(4));

            // Top right, bottom left corners
            rectangle = new Rectangle(topRight, bottomLeft);

            Assert.That(rectangle.BottomLeft, Is.EqualTo(bottomLeft));
            Assert.That(rectangle.TopRight, Is.EqualTo(topRight));
            Assert.That(rectangle.Width, Is.EqualTo(3));
            Assert.That(rectangle.Height, Is.EqualTo(4));

            // Top left, bottom right corners
            rectangle = new Rectangle(topLeft, bottomRight);

            Assert.That(rectangle.BottomLeft, Is.EqualTo(bottomLeft));
            Assert.That(rectangle.TopRight, Is.EqualTo(topRight));
            Assert.That(rectangle.Width, Is.EqualTo(3));
            Assert.That(rectangle.Height, Is.EqualTo(4));
        }

        [Test]
        public void Contains_GivenAPositionWithinTheRectangleIncludingTheEdgesOfTheRectangle_ReturnsTrue()
        {
            Position bottomLeft = new Position(1, 1);
            Rectangle rectangle = new Rectangle(bottomLeft, 5, 4);

            Assert.That(rectangle.Contains(new Position(2, 3)), Is.True);
            Assert.That(rectangle.Contains(new Position(1, 1)), Is.True);
            Assert.That(rectangle.Contains(new Position(5, 1)), Is.True);
            Assert.That(rectangle.Contains(new Position(1, 4)), Is.True);
            Assert.That(rectangle.Contains(new Position(5, 4)), Is.True);
        }

        [Test]
        public void Contains_GivenAPositionOutsideTheRectangle_ReturnsFalse()
        {
            Position bottomLeft = new Position(1, 1);
            Rectangle rectangle = new Rectangle(new Position(1, 1), 5, 4);

            Assert.That(rectangle.Contains(new Position(0, 0)), Is.False);
            Assert.That(rectangle.Contains(new Position(0, 1)), Is.False);
            Assert.That(rectangle.Contains(new Position(6, 1)), Is.False);
            Assert.That(rectangle.Contains(new Position(1, 5)), Is.False);
            Assert.That(rectangle.Contains(new Position(5, 6)), Is.False);
        }

        [Test]
        public void RectangleImplementsAnIterator_ThatReturnsAllPositionsContainedInTheRectangle()
        {
            Rectangle rectangle = new Rectangle(new Position(1, 1), 2, 3);
            var iteratedPositions = new List<Position>();

            foreach(Position position in rectangle)
            {
                iteratedPositions.Add(position);
            }

            Assert.That(iteratedPositions.Count, Is.EqualTo(6));
            // Check that the positions returned were iterated from left to right and bottom to top of the rectangle.
            Assert.That(iteratedPositions[0], Is.EqualTo(new Position(1, 1)));
            Assert.That(iteratedPositions[1], Is.EqualTo(new Position(2, 1)));
            Assert.That(iteratedPositions[2], Is.EqualTo(new Position(1, 2)));
            Assert.That(iteratedPositions[3], Is.EqualTo(new Position(2, 2)));
            Assert.That(iteratedPositions[4], Is.EqualTo(new Position(1, 3)));
            Assert.That(iteratedPositions[5], Is.EqualTo(new Position(2, 3)));
        }

        [Test]
        public void MoveTo_GivenANewPositionForTheBottomLeftCorner_MovesTheRectanglesBottomLeftCorner()
        {
            Position newPosition = new Position(3, 3);
            Rectangle rectangle = new Rectangle(new Position(1, 1), 2, 3);

            rectangle.MoveTo(newPosition);
 
            Assert.That(rectangle.BottomLeft, Is.EqualTo(newPosition));
            Assert.That(rectangle.TopRight, Is.EqualTo(new Position(newPosition.X + 2 - 1, newPosition.Y + 3 - 1)));
            Assert.That(rectangle.Width, Is.EqualTo(2));
            Assert.That(rectangle.Height, Is.EqualTo(3));
        }
    }
}