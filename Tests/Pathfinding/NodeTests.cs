using NUnit.Framework;
using System;
using Turnable.Pathfinding;
using Turnable.Utilities;

namespace Tests.Pathfinding
{
    [TestFixture]
    public class NodeTests
    {
        private Node node;

        [Test]
        public void Constructor_GivenAPositionAndANullParent_InitializesANode()
        {
            var node = new Node(new Position(0, 0));

            Assert.That(node.Position, Is.EqualTo(new Position(0, 0)));
            Assert.That(node.Parent, Is.Null);
        }

        // *********************************************************************
        // IEquatable<T> implementation tests
        // REF: https://msdn.microsoft.com/en-us/library/ms131190(v=vs.110).aspx
        // *********************************************************************
        // Equals Tests
        [Test]
        public void Equals_FromIEquatableTInterface_CanCompareNodes()
        {
            Node node = new Node(new Position(1, 2));
            Node node2 = new Node(new Position(1, 2));

            Assert.That(node.Equals(node2), Is.True);

            node2 = new Node(new Position(2, 3));
            Assert.That(node.Equals(node2), Is.False);
        }

        [Test]
        public void Equals_FromIEquatableTInterface_CanCompareNodeToNull()
        {
            Node node = new Node(new Position(1, 2));

            Assert.That(node.Equals(null), Is.False);
        }

        [Test]
        public void Equals_OverridenFromObjectEquals_CanCompareNodes()
        {
            Object node = new Node(new Position(1, 2));
            Object node2 = new Node(new Position(1, 2));

            Assert.That(node.Equals(node2), Is.True);

            node2 = new Node(new Position(2, 3));
            Assert.That(node.Equals(node2), Is.False);
        }

        [Test]
        public void Equals_OverridenFromObjectEquals_CanCompareNodeToNull()
        {
            Object node = new Node(new Position(1, 2));

            Assert.That(node.Equals(null), Is.False);
        }

        [Test]
        public void Equals_OverridenFromObjectEquals_ReturnsFalseIfOtherObjectIsNotANode()
        {
            Object node = new Node(new Position(1, 2));

            Assert.That(node.Equals(new Object()), Is.False);
        }

        [Test]
        public void EqualityOperator_IsImplemented()
        {
            Node node = new Node(new Position(1, 2));
            Node node2 = new Node(new Position(1, 2));

            Assert.That(node == node2, Is.True);
        }

        [Test]
        public void InequalityOperator_IsImplemented()
        {
            Node node = new Node(new Position(1, 2));
            Node node2 = new Node(new Position(2, 3));

            Assert.That(node != node2, Is.True);
        }

        [Test]
        public void EqualityOperator_CanComparePositionToNull()
        {
            Node node = null;

            Assert.That(node == null, Is.True);
        }

        [Test]
        public void InequalityOperator_CanComparePositionToNull()
        {
            Node node = new Node(new Position(1, 2));

            Assert.That(node != null, Is.True);
        }

        [Test]
        public void GetHashCode_UsesThePositionsHashCode()
        {
            Node node = new Node(new Position(1, 2));

            Assert.That(node.GetHashCode(), Is.EqualTo(node.Position.GetHashCode()));
        }
    }
}