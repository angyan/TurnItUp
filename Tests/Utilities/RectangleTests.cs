using System;
using NUnit.Framework;
using Turnable.Utilities;

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
    }
}