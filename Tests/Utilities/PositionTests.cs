using System;
using NUnit.Framework;
using Turnable.Utilities;

namespace Tests.Utilities
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void ParameterlessConstructor_InitializesXAndYCoordinatesTo0()
        {
            Position position = new Position();

            Assert.That(position.X, Is.EqualTo(0));
            Assert.That(position.Y, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_GivenXAndY_InitializesXAndY()
        {
            Position position = new Position(1, 2);

            Assert.That(position.X, Is.EqualTo(1));
            Assert.That(position.Y, Is.EqualTo(2));
        }

        [Test]
        public void Clone_CreatesAClone()
        {
            Position position = new Position(1, 2);
            Position clonedPosition = position.Clone();

            Assert.That(position, Is.Not.SameAs(clonedPosition));
            Assert.That(clonedPosition.X, Is.EqualTo(position.X));
            Assert.That(clonedPosition.Y, Is.EqualTo(position.Y));
        }

        // *********************************************************************
        // IEquatable<T> implementation tests
        // REF: https://msdn.microsoft.com/en-us/library/ms131190(v=vs.110).aspx
        // *********************************************************************
        [Test]
        public void Equals_FromIEquatableTInterface_CanCompareEqualityBasedOnXAndY()
        {
            Position position = new Position(1, 2);
            Position position2 = new Position(1, 2);

            Assert.That(position.Equals(position2), Is.True);

            position2 = new Position(2, 3);
            Assert.That(position.Equals(position2), Is.False);
        }

        [Test]
        public void Equals_FromIEquatableTInterface_CanComparePositionToNull()
        {
            Position position = new Position(1, 2);

            Assert.That(position.Equals(null), Is.False);
        }

        [Test]
        public void Equals_OverridingFromBaseClassImplementation_CanCompareEqualityBasedOnXAndY()
        {
            Object position = new Position(1, 2);
            Object position2 = new Position(1, 2);

            Assert.That(position.Equals(position2), Is.True);

            position2 = new Position(2, 3);
            Assert.That(position.Equals(position2), Is.False);
        }

        [Test]
        public void Equals_OverridingBaseClassImplementation_CanComparePositionToNull()
        {
            Object position = new Position(1, 2);

            Assert.That(position.Equals(null), Is.False);
        }

        [Test]
        public void Equals_OverridingBaseClassImplementation_ReturnsFalseIfOtherObjectIsNotAPosition()
        {
            Object position = new Position(1, 2);

            Assert.That(position.Equals(new Object()), Is.False);
        }

        [Test]
        public void EqualityOperator_IsImplemented()
        {
            Position position = new Position(1, 2);
            Position position2 = new Position(1, 2);

            Assert.That(position == position2, Is.True);
        }

        [Test]
        public void InequalityOperator_IsImplemented()
        {
            Position position = new Position(1, 2);
            Position position2 = new Position(2, 3);

            Assert.That(position != position2, Is.True);
        }

        [Test]
        public void EqualityOperator_CanComparePositionToNull()
        {
            Position position = null;

            Assert.That(position == null, Is.True);
        }

        [Test]
        public void InequalityOperator_CanComparePositionToNull()
        {
            Position position = new Position(1, 2);

            Assert.That(position != null, Is.True);
        }

        [Test]
        public void GetHashCode_IsOverridenToReturnASuitableHashCode()
        {
            Position position = new Position(1, 2);
            int calculatedHash;

            // REF: http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = hash * 16777619 ^ position.X.GetHashCode();
                hash = hash * 16777619 ^ position.Y.GetHashCode();
                calculatedHash = hash;
            }

            Assert.That(position.GetHashCode(), Is.EqualTo(calculatedHash));
        }

        [Test]
        public void ToString_DisplaysXAndYCoordinates()
        {
            Position position = new Position(4, 5);

            Assert.That(position.ToString(), Is.EqualTo("Position <X: 4; Y: 5>"));
        }

        [Test]
        public void NeighboringPosition_GivenADirection_ReturnsTheNearestNeighboringPositionInThatDirection()
        {
            Position position = new Position(4, 5);

            Assert.That(position.NeighboringPosition(Direction.North), Is.EqualTo(new Position(4, 6)));
            Assert.That(position.NeighboringPosition(Direction.NorthEast), Is.EqualTo(new Position(5, 6)));
            Assert.That(position.NeighboringPosition(Direction.East), Is.EqualTo(new Position(5, 5)));
            Assert.That(position.NeighboringPosition(Direction.SouthEast), Is.EqualTo(new Position(5, 4)));
            Assert.That(position.NeighboringPosition(Direction.South), Is.EqualTo(new Position(4, 4)));
            Assert.That(position.NeighboringPosition(Direction.SouthWest), Is.EqualTo(new Position(3, 4)));
            Assert.That(position.NeighboringPosition(Direction.West), Is.EqualTo(new Position(3, 5)));
            Assert.That(position.NeighboringPosition(Direction.NorthWest), Is.EqualTo(new Position(3, 6)));
        }
    }
}