using NUnit.Framework;
using System.Collections.Generic;
using Turnable.Pathfinding;
using Turnable.Utilities;

namespace Tests.Pathfinding
{
    [TestFixture]
    public class PathfinderTests
    {
        private Pathfinder pathfinder;
        private List<Position> obstacles;

        [SetUp]
        public void SetUp()
        {
            var bounds = new Rectangle(new Position(0, 0), 10, 10);

            pathfinder = new Pathfinder(bounds);
            obstacles = new List<Position>();
        }

        [Test]
        public void Constructor_GivenARectangle_InitializesAPathfinderWithTheRectangleAsBoundsAndAllowsDiagonalMovement()
        {
            var bounds = new Rectangle(new Position(0, 0), 5, 10);

            var pathfinder = new Pathfinder(bounds);

            Assert.That(pathfinder.Bounds, Is.EqualTo(bounds));
            Assert.That(pathfinder.AllowDiagonalMovement, Is.EqualTo(true));
        }

        [Test]
        public void Constructor_GivenARectangleAndDisallowingDiagonalMovement_InitializesAPathfinderWithTheRectangleAsBoundsAndDisallowsDiagonalMovement()
        {
            var bounds = new Rectangle(new Position(0, 0), 5, 10);

            var pathfinder = new Pathfinder(bounds, false);

            Assert.That(pathfinder.Bounds, Is.EqualTo(bounds));
            Assert.That(pathfinder.AllowDiagonalMovement, Is.EqualTo(false));
        }

        // *******************************************
        // Pathfinding with diagonal movement allowed
        // *******************************************
        [Test]
        public void FindPath_GivenAnUnwalkableEndPosition_ThrowsAnInvalidOperationException()
        {
            obstacles.Add(new Position(1, 1));

            Assert.That(() => pathfinder.FindPath(new Position(0, 0), new Position(1, 1), obstacles), Throws.InvalidOperationException);
        }

        [Test]
        public void FindPath_GivenAStartAndEndPositionsWhichAreOrthogonalAndNextToEachOther_FindsPath()
        {
            List<Position> path = pathfinder.FindPath(new Position(1, 1), new Position(1, 2), obstacles);

            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(2));
            Assert.That(path[0], Is.EqualTo(new Position(1, 1)));
            Assert.That(path[1], Is.EqualTo(new Position(1, 2)));
        }

        [Test]
        public void FindPath_GivenAStartAndEndPositionWhichAreDiagonalAndNextToEachOther_FindsPath()
        {
            List<Position> path = pathfinder.FindPath(new Position(6, 6), new Position(5, 5), obstacles);

            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(2));
            Assert.That(path[0], Is.EqualTo(new Position(6, 6)));
            Assert.That(path[1], Is.EqualTo(new Position(5, 5)));
        }

        [Test]
        public void FindPath_GivenAStartAndEndPositionWhichAreOrthogonallySeparatedWithNoObstaclesBetweenThem_FindsPath()
        {
            List<Position> path = pathfinder.FindPath(new Position(1, 1), new Position(4, 1), obstacles);

            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(4));
            Assert.That(path[0], Is.EqualTo(new Position(1, 1)));
            Assert.That(path[1], Is.EqualTo(new Position(2, 1)));
            Assert.That(path[2], Is.EqualTo(new Position(3, 1)));
            Assert.That(path[3], Is.EqualTo(new Position(4, 1)));
        }

        [Test]
        public void FindPath_GivenAStartAndEndPositionWhichAreDiagonallySeparatedWithNoObstaclesBetweenThem_FindsPath()
        {
            List<Position> path = pathfinder.FindPath(new Position(3, 5), new Position(5, 7), obstacles);

            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(3));
            Assert.That(path[0], Is.EqualTo(new Position(3, 5)));
            Assert.That(path[1], Is.EqualTo(new Position(4, 6)));
            Assert.That(path[2], Is.EqualTo(new Position(5, 7)));
        }

        [Test]
        public void FindPath_GivenAStartAndEndPositionWhichAreOrthogonallySeparatedAndHaveOneObstacleBetweenThem_FindsPath()
        {
            // Obstacle at (6, 5)
            obstacles.Add(new Position(6, 5));
            List<Position> path = pathfinder.FindPath(new Position(6, 3), new Position(6, 7), obstacles);

            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(5));
            Assert.That(path[0], Is.EqualTo(new Position(6, 3)));
            Assert.That(path[1], Is.EqualTo(new Position(6, 4)));
            Assert.That(path[2], Is.EqualTo(new Position(7, 5)));
            Assert.That(path[3], Is.EqualTo(new Position(7, 6)));
            Assert.That(path[4], Is.EqualTo(new Position(6, 7)));
        }

        [Test]
        public void FindPath_GivenAStartAndEndPositionWhichAreDiagonallySeparatedAndHaveOneObstacleBetweenThem_FindsPath()
        {
            // Obstacle at (6, 5)
            obstacles.Add(new Position(6, 5));
            List<Position> path = pathfinder.FindPath(new Position(4, 3), new Position(8, 7), obstacles);

            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(6));
            Assert.That(path[0], Is.EqualTo(new Position(4, 3)));
            Assert.That(path[1], Is.EqualTo(new Position(4, 4)));
            Assert.That(path[2], Is.EqualTo(new Position(5, 5)));
            Assert.That(path[3], Is.EqualTo(new Position(6, 6)));
            Assert.That(path[4], Is.EqualTo(new Position(7, 7)));
            Assert.That(path[5], Is.EqualTo(new Position(8, 7)));
        }

        //[Test]
        //public void FindPath_WhenEndingNodeIsUnreachable_ReturnsNull()
        //{
        //    Assert.That(_pathfinderWithDiagonalMovement.FindPath(new Node(_level, 5, 5), new Node(_level, 13, 13)), Is.Null);
        //}
    }
}