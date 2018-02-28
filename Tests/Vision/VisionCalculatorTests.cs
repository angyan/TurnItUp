using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Turnable.Characters;
using Turnable.Places;
using Turnable.Utilities;
using Turnable.Vision;

namespace Tests.Places
{
    [TestFixture]
    public class VisionCalculatorTests
    {
        private VisionCalculator visionCalculator;
        private List<Position> obstacles;

        [SetUp]
        public void SetUp()
        {
            obstacles = new List<Position>();
            var bounds = new Rectangle(new Position(0, 0), 10, 10);

            visionCalculator = new VisionCalculator(bounds);
        }

        [Test]
        public void Constructor_GivenABounds_InitializesAVisionCalculatorWithTheBoundsAndDisallowsDiagonalMovement()
        {
            var bounds = new Rectangle(new Position(0, 0), 5, 10);

            var visionCalculator = new VisionCalculator(bounds);

            Assert.That(visionCalculator.Bounds, Is.EqualTo(bounds));
        }

        [Test]
        public void CalculateSlope_GivenTwoPositions_CalculatesTheSlope()
        {
            double slope = visionCalculator.Slope(0, 0, 1, 1);

            Assert.That(slope, Is.EqualTo(1.0));
        }

        [Test]
        public void CalculateSlope_GivenTwoPositions_CalculatesTheInverseSlope()
        {
            double slope = visionCalculator.Slope(4, 2, 3, 4, true);

            Assert.That(slope, Is.EqualTo(-2.0));
        }

        [Test]
        public void CalculateVisibleDistance_CorrectlyCalculatesTheSquaredDistanceBetweenTwoPoints()
        {
            double visibleDistance = visionCalculator.VisibleDistance(0, 0, 1, 1);
            Assert.That(visibleDistance, Is.EqualTo(1.414).Within(0.001));

            visibleDistance = visionCalculator.VisibleDistance(4, 2, 3, 4);
            Assert.That(visibleDistance, Is.EqualTo(2.236).Within(0.001));
        }

        //-------------------------
        // FOV Calculation Examples
        //-------------------------

        [Test]
        public void CalculateFieldOfView_GivenAVisualRangeOf0_ReturnsOnlyTheStart()
        {
            List<Position> fieldOfView = visionCalculator.CalculateFieldOfView(new Position(7, 14), 0, obstacles);

            Assert.That(fieldOfView.Count, Is.EqualTo(1));
            Assert.That(fieldOfView.Contains(new Position(7, 14)), Is.True);
        }

        [Test]
        public void VisionCalculator_ForAVisualRangeOf1AndNoObstacles_ReturnsTheStartingPositionAndAllPositionsOrthogonallyAdjacentToTheStartingPosition()
        {
            // The FOV algorithm creates a cross for a VisualRange of 1
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(7, 3), 1, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(5));
            Assert.That(visiblePositions.Contains(new Position(7, 3)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(7, 2)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(7, 4)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 3)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(8, 3)), Is.True);
        }

        [Test]
        public void VisionCalculator_ForAVisualRangeOf1_IncludesObstaclesInTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            obstacles.Add(new Position(7, 4));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(6, 4), 1, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(5));
            Assert.That(visiblePositions.Contains(new Position(6, 4)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 3)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(5, 4)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(7, 4)), Is.True);
        }

        // Testing with an obstacle in each of the eight directions right next to the start and with a visual range of 2
        // With a VisualRange of 2 using the current algorithm, only obstacles directly to the E, N, W or S will block off a visible position
        //   X
        //  XXX
        // XXOXX
        //  XXX
        //   X
        // With a Visual Range of 2, the FOV is every adjacent tile as well as two tiles in the E, N, W and S direction. 

        // Obstacle to the N
        [Test]
        public void CalculateVisiblePositions_ForAVisualRangeOf2AndObstacleToTheNorth_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(6, 4), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(12));
            Assert.That(visiblePositions.Contains(new Position(6, 4)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 6)), Is.False);
        }

        // Obstacle to the NE
        [Test]
        public void CalculateVisiblePositions_ForAVisualRangeOf2AndObstacleToTheNorthEast_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(5, 4), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(13));
            Assert.That(visiblePositions.Contains(new Position(5, 4)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
        }

        // Obstacle to the E
        [Test]
        public void CalculateVisiblePositions_ForAVisualRangeOf2AndObstacleToTheEast_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(5, 5), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(12));
            Assert.That(visiblePositions.Contains(new Position(5, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(7, 5)), Is.False);
        }

        // Obstacle to the SE
        [Test]
        public void CalculateVisiblePositions_ForAVisualRangeOf2AndObstacleToTheSouthEast_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(5, 6), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(13));
            Assert.That(visiblePositions.Contains(new Position(5, 6)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
        }

        // Obstacle to the S
        [Test]
        public void CalculateVisiblePositions_ForAVisualRangeOf2AndObstacleToTheSouth_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(6, 6), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(12));
            Assert.That(visiblePositions.Contains(new Position(6, 6)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 4)), Is.False);
        }

        // Obstacle to the SW
        [Test]
        public void VisionCalculator_ForAVisualRangeOf2AndObstacleToTheSouthWest_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(7, 6), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(13));
            Assert.That(visiblePositions.Contains(new Position(7, 6)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
        }

        // Obstacle to the W
        [Test]
        public void VisionCalculator_ForAVisualRangeOf2AndObstacleToTheWest_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(7, 5), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(12));
            Assert.That(visiblePositions.Contains(new Position(7, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(5, 5)), Is.False);
        }

        // Obstacle to the NW
        [Test]
        public void VisionCalculator_ForAVisualRangeOf2AndObstacleToTheNorthWest_CorrectlyCalculatesTheVisiblePositions()
        {
            obstacles.Add(new Position(6, 5));
            List<Position> visiblePositions = visionCalculator.CalculateFieldOfView(new Position(7, 4), 2, obstacles);

            Assert.That(visiblePositions.Count, Is.EqualTo(13));
            Assert.That(visiblePositions.Contains(new Position(7, 4)), Is.True);
            Assert.That(visiblePositions.Contains(new Position(6, 5)), Is.True);
        }
    }
}