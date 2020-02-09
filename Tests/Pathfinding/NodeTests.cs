using NUnit.Framework;
using System;
using System.Collections.Generic;
using Turnable.Pathfinding;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Pathfinding
{
    [TestFixture]
    public class NodeTests
    {
        private Rectangle levelBounds;

        [SetUp]
        public void SetUp()
        {
            levelBounds = new Rectangle(new Position(0, 0), new Position(15, 15));
        }

        [Test]
        public void Constructor_GivenLevelBoundsPositionAndNoParentNode_InitializesANode()
        {
            var node = new Node(levelBounds, new Position(0, 0));

            Assert.That(node.LevelBounds, Is.EqualTo(levelBounds));
            Assert.That(node.Position, Is.EqualTo(new Position(0, 0)));
            Assert.That(node.Parent, Is.Null);
        }

        [Test]
        public void Constructor_GivenLevelBoundsPositionAndAParentNode_InitializesNode()
        {
            Node parentNode = new Node(levelBounds, new Position(0, 0));

            Node node = new Node(levelBounds, new Position(0, 0), parentNode);

            Assert.That(node.Position, Is.EqualTo(new Position(0, 0)));
            Assert.That(node.Parent, Is.SameAs(parentNode));
        }

        [Test]
        public void IsWithinBounds_GivenANode_ReturnsWhetherTheNodesPositionIsWithinTheBoundsOfTheLevel()
        {
            Node node = new Node(levelBounds, new Position(7, 7));
            Assert.That(node.IsWithinBounds(), Is.True);

            node = new Node(levelBounds, new Position(0, 0));
            Assert.That(node.IsWithinBounds(), Is.True);

            node = new Node(levelBounds, new Position(14, 14));
            Assert.That(node.IsWithinBounds(), Is.True);

            node = new Node(levelBounds, new Position(20, 4));
            Assert.That(node.IsWithinBounds(), Is.False);

            node = new Node(levelBounds, new Position(-1, -1));
            Assert.That(node.IsWithinBounds(), Is.False);

            node = new Node(levelBounds, new Position(16, 16));
            Assert.That(node.IsWithinBounds(), Is.False);
        }

        [Test]
        public void AdjacentNodes_ReturnsAListContainingAllAdjacentNodes()
        {
            Node node = new Node(levelBounds, new Position(5, 5));

            List<Position> adjacentNodePositions = node.AdjacentNodes().ConvertAll<Position>(n => n.Position);

            Assert.That(adjacentNodePositions.Count, Is.EqualTo(8));
            Assert.That(adjacentNodePositions.Contains(new Position(4, 4)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(5, 4)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(6, 4)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(4, 5)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(4, 6)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(5, 6)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(6, 6)), Is.True);
        }

        [Test]
        public void AdjacentNodes_DisregardsNodesThatAreOutOfBounds()
        {
            Node node = new Node(levelBounds, new Position(0, 0));

            List<Position> adjacentNodePositions = node.AdjacentNodes().ConvertAll<Position>(n => n.Position);

            Assert.That(adjacentNodePositions.Count, Is.EqualTo(3));
            Assert.That(adjacentNodePositions.Contains(new Position(1, 0)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(0, 1)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(1, 1)), Is.True);
        }

        [Test]
        public void AdjacentNodes_WhenDiagonalMovementIsNotAllowed_OnlyReturnsAdjacentOrthogonalNodes()
        {
            var node = new Node(levelBounds, new Position(5, 5));

            List<Position> adjacentNodePositions = node.AdjacentNodes(false).ConvertAll<Position>(n => n.Position);

            Assert.That(adjacentNodePositions.Count, Is.EqualTo(4));
            Assert.That(adjacentNodePositions.Contains(new Position(5, 4)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(4, 5)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(adjacentNodePositions.Contains(new Position(5, 6)), Is.True);
        }

        // ***************************************************************************************
        // Calculations for A* algorithm (PathScore, EstimatedMovementCost and ActualMovementCost)
        // ***************************************************************************************
        [Test]
        public void ActualMovementCost_ForANodeWithoutAParent_Is0()
        {
            var node = new Node(levelBounds, new Position(0, 0));

            Assert.That(node.ActualMovementCost, Is.EqualTo(0));
        }

        //[Test]
        //public void EstimatedMovementCost_ForANodeWithoutAParent_Is0()
        //{
        //    var node = new Node(levelBounds, new Position(0, 0));

        //    Assert.That(node.EstimatedMovementCost, Is.EqualTo(0));
        //}

        //[Test]
        //public void EstimatedMovementCost_GivenAValue_CanBeSet()
        //{
        //    var node = new Node(levelBounds, new Position(0, 0));

        //    node.EstimatedMovementCost = 10;

        //    Assert.That(node.EstimatedMovementCost, Is.EqualTo(10));
        //}

        [Test]
        public void ActualMovementCost_WithAnOrthogonalParentNode_HasAnOrthogonalMovementCost()
        {
            var parentNode = new Node(levelBounds, new Position(5, 5));
            Direction[] orthogonalDirections = new Direction[4] { Direction.North, Direction.East, Direction.South, Direction.East };

            foreach (Direction direction in orthogonalDirections)
            {
                Node node = new Node(levelBounds, parentNode.Position.NeighboringPosition(direction), parentNode);

                Assert.That(node.ActualMovementCost, Is.EqualTo(parentNode.ActualMovementCost + Node.OrthogonalMovementCost));
            }
        }

        [Test]
        public void ActualMovementCost_WithDiagonalParentNode_HasADiagonalMovementCost()
        {
            var parentNode = new Node(levelBounds, new Position(5, 5));
            Direction[] diagonalDirections = new Direction[4] { Direction.NorthEast, Direction.SouthEast, Direction.SouthWest, Direction.NorthWest };

            foreach (Direction direction in diagonalDirections)
            {
                Node node = new Node(levelBounds, parentNode.Position.NeighboringPosition(direction), parentNode);

                Assert.That(node.ActualMovementCost, Is.EqualTo(parentNode.ActualMovementCost + Node.DiagonalMovementCost));
            }
        }

        [Test]
        public void CalculateEstimatedMovementCost_CalculatesAndStoresTheManhattanDistanceBetweenTwoPositions()
        {
            // Manhattan distance = (Sum of the horizontal and vertical distance) * OrthogonalMovementCost
            Node node = new Node(levelBounds, new Position(5, 5), null);

            Node otherNode = new Node(levelBounds, new Position(4, 4), null);
            node.CalculateEstimatedMovementCost(otherNode);
            Assert.That(node.EstimatedMovementCost, Is.EqualTo(20));

            otherNode = new Node(levelBounds, new Position(4, 5), null);
            node.CalculateEstimatedMovementCost(otherNode);
            Assert.That(node.EstimatedMovementCost, Is.EqualTo(10));

            otherNode = new Node(levelBounds, new Position(5, 4), null);
            node.CalculateEstimatedMovementCost(otherNode);
            Assert.That(node.EstimatedMovementCost, Is.EqualTo(10));
        }
        // *********************************************************************
        // IEquatable<T> implementation tests
        // REF: https://msdn.microsoft.com/en-us/library/ms131190(v=vs.110).aspx
        // *********************************************************************
        // Equals Tests
        [Test]
        public void Equals_FromIEquatableTInterface_CanCompareNodes()
        {
            Node node = new Node(levelBounds, new Position(1, 2));
            Node node2 = new Node(levelBounds, new Position(1, 2));

            Assert.That(node.Equals(node2), Is.True);

            node2 = new Node(levelBounds, new Position(2, 3));
            Assert.That(node.Equals(node2), Is.False);
        }

        [Test]
        public void Equals_FromIEquatableTInterface_CanCompareNodeToNull()
        {
            Node node = new Node(levelBounds, new Position(1, 2));

            Assert.That(node.Equals(null), Is.False);
        }

        [Test]
        public void Equals_OverridenFromObjectEquals_CanCompareNodes()
        {
            Object node = new Node(levelBounds, new Position(1, 2));
            Object node2 = new Node(levelBounds, new Position(1, 2));

            Assert.That(node.Equals(node2), Is.True);

            node2 = new Node(levelBounds, new Position(2, 3));
            Assert.That(node.Equals(node2), Is.False);
        }

        [Test]
        public void Equals_OverridenFromObjectEquals_CanCompareNodeToNull()
        {
            Object node = new Node(levelBounds, new Position(1, 2));

            Assert.That(node.Equals(null), Is.False);
        }

        [Test]
        public void Equals_OverridenFromObjectEquals_ReturnsFalseIfOtherObjectIsNotANode()
        {
            Object node = new Node(levelBounds, new Position(1, 2));

            Assert.That(node.Equals(new Object()), Is.False);
        }

        [Test]
        public void EqualityOperator_IsImplemented()
        {
            Node node = new Node(levelBounds, new Position(1, 2));
            Node node2 = new Node(levelBounds, new Position(1, 2));

            Assert.That(node == node2, Is.True);
        }

        [Test]
        public void InequalityOperator_IsImplemented()
        {
            Node node = new Node(levelBounds, new Position(1, 2));
            Node node2 = new Node(levelBounds, new Position(2, 3));

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
            Node node = new Node(levelBounds, new Position(1, 2));

            Assert.That(node != null, Is.True);
        }

        [Test]
        public void GetHashCode_UsesThePositionsHashCode()
        {
            Node node = new Node(levelBounds, new Position(1, 2));

            Assert.That(node.GetHashCode(), Is.EqualTo(node.Position.GetHashCode()));
        }
    }
}