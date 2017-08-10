using System;
using NUnit.Framework;
using Turnable.Utilities;
using System.IO;
using Turnable.Places;

namespace Tests.Places
{
    [TestFixture]
    public class ViewportTests
    {
        private Level level;

        [SetUp]
        public void SetUp()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");
            level = new Level(fullPath);
        }

        [Test]
        public void Viewport_ImplementsTheIBoundedInterface()
        {
            var viewport = new Viewport();

            Assert.That(viewport, Is.InstanceOf<IBounded>());
        }

        [Test]
        public void Constructor_GivenAWidthAndHeight_InitializesTheViewport()
        {
            var viewport = new Viewport(16, 15);

            Assert.That(viewport.Bounds, Is.Not.Null);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
            Assert.That(viewport.Bounds.Width, Is.EqualTo(16));
            Assert.That(viewport.Bounds.Height, Is.EqualTo(15));
        }


        [Test]
        public void Constructor_GivenAPositionAndSize_InitializesTheViewport()
        {
            var tileMapPosition = new Position(54, 53);
            var viewport = new Viewport(16, 15, tileMapPosition);

            Assert.That(viewport.Bounds, Is.Not.Null);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(tileMapPosition));
            Assert.That(viewport.Bounds.Width, Is.EqualTo(16));
            Assert.That(viewport.Bounds.Height, Is.EqualTo(15));
        }

        //[Test]
        //public void IsMapOriginValid_WhenViewportHasAMapOriginThatIsOutOfBounds_ReturnsFalse()
        //{
        //    List<Position> invalidMapOrigins = new List<Position>();

        //    ((IBounded)_viewport).Bounds.Move(new Position(-1, 0));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(0, -1));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(11, 0));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(10, -1));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(10, 11));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(11, 10));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(-1, 11));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);

        //    ((IBounded)_viewport).Bounds.Move(new Position(0, 11));
        //    Assert.That(_viewport.IsMapLocationValid(), Is.False);
        //}

        // -------------------
        // Move viewport tests
        // -------------------
        [Test]
        public void MoveTo_GivenANewPositionWhenTheViewportRemainsFullyWithinBoundsOfTheTileMap_MovesTheViewportToTheNewPosition()
        {
            Position originalViewportPosition = new Position(3, 3);
            Position newViewportPosition = new Position(4, 4);
            Viewport viewport = new Viewport(6, 6, originalViewportPosition);

            viewport.MoveTo(newViewportPosition);

            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(newViewportPosition));
        }

        [Test]
        public void MoveInDirection_GivenADirectionWhenTheViewportRemainsFullyWithinBoundsOfTheTileMap_MovesTheViewportInTheGivenDirection()
        {
            Position originalViewportPosition = new Position(3, 3);
            Viewport viewport = new Viewport(6, 6, originalViewportPosition);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                viewport.MoveInDirection(direction, level.TileMap.Bounds);

                Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(originalViewportPosition.NeighboringPosition(direction)));

                viewport.MoveTo(originalViewportPosition);
            }
        }

        [Test]
        public void MoveInDirection_WhenViewportWouldMoveViewportOutOfTileMapBounds_MovesAsMuchAsPossible()
        {
            Viewport viewport = new Viewport(6, 6);

            // Viewport at lower left of the Map
            viewport.MoveTo(new Position(0, 0));

            viewport.MoveInDirection(Direction.South, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
            viewport.MoveInDirection(Direction.SouthWest, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
            viewport.MoveInDirection(Direction.West, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
            viewport.MoveInDirection(Direction.NorthWest, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 1)));

            // Viewport at bottom right of the Map
            viewport.MoveTo(new Position(level.TileMap.Bounds.Width - viewport.Bounds.Width, 0));
            Console.Write(level.TileMap.Bounds);

            viewport.MoveInDirection(Direction.South, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 0)));
            viewport.MoveInDirection(Direction.SouthEast, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 0)));
            viewport.MoveInDirection(Direction.East, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 0)));
            viewport.MoveInDirection(Direction.NorthEast, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 1)));

            // Viewport at top right of the Map
            viewport.MoveTo(new Position(level.TileMap.Bounds.Width - viewport.Bounds.Width, level.TileMap.Bounds.Height - viewport.Bounds.Height));

            viewport.MoveInDirection(Direction.SouthEast, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 24)));
            viewport.MoveInDirection(Direction.East, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 24)));
            viewport.MoveInDirection(Direction.NorthEast, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 25)));
            viewport.MoveInDirection(Direction.North, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(39, 25)));

            // Viewport at top left of the Map
            viewport.MoveTo(new Position(0, level.TileMap.Bounds.Height - viewport.Bounds.Height));

            viewport.MoveInDirection(Direction.North, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 25)));
            viewport.MoveInDirection(Direction.NorthWest, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 25)));
            viewport.MoveInDirection(Direction.West, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 25)));
            viewport.MoveInDirection(Direction.SouthWest, level.TileMap.Bounds);
            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 24)));
        }

        ////// Testing the automatic movement of MapOrigin when the player moves
        ////// Enough space on all sides to allow movement of MapOrigin
        ////// Viewport with odd height and width
        ////[Test]
        ////public void Viewport_MovingPlayerLocateadAtTheExactCenterOfTheViewport_MovesTheMapOriginOfViewportInThatSameDirection()
        ////{
        ////    _level.SetUpViewport(5, 1, 5, 5);
        ////    _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(7, 3));

        ////    foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        ////    {
        ////        _level.MovePlayer(direction);
        ////        Assert.That(2, Math.Abs(_level.Viewport.MapOrigin.X - _level.CharacterManager.Player.GetComponent<Position>().X));
        ////        Assert.That(2, Math.Abs(_level.Viewport.MapOrigin.Y - _level.CharacterManager.Player.GetComponent<Position>().Y));

        ////        // Reset viewport and player's position
        ////        _level.Viewport.MapOrigin.X = 5;
        ////        _level.Viewport.MapOrigin.Y = 1;
        ////        _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(7, 3));
        ////    }
        ////}

        ////[Test]
        ////public void Viewport_MovingPlayerWhenLocatedAtTheCentralRowInAnyColumnOfTheViewport_MovesTheMapOriginCorrectly()
        ////{
        ////    _level.SetUpViewport(5, 1, 5, 5);
        ////    _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(5, 3));

        ////    // If player moves North or South, the viewport should shift North or South respectively
        ////    _level.MovePlayer(Direction.North);
        ////    Assert.That(5, _level.Viewport.MapOrigin.X);
        ////    Assert.That(2, _level.Viewport.MapOrigin.Y);
        ////    _level.MovePlayer(Direction.South);
        ////    Assert.That(5, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);

        ////    // If player Moves NorthEast Or SouthEast, the viewport just shifts North or South respectively
        ////    // It should not shift to the East as the player is still not located at the central column of the viewport
        ////    _level.MovePlayer(Direction.NorthEast);
        ////    Assert.That(5, _level.Viewport.MapOrigin.X);
        ////    Assert.That(2, _level.Viewport.MapOrigin.Y);
        ////    _level.MovePlayer(Direction.SouthEast);
        ////    Assert.That(5, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);
        ////}

        ////[Test]
        ////public void Viewport_MovingPlayerWhenLocatedAtTheCentralColumnInAnyRowOfTheViewport_MovesTheMapOriginCorrectly()
        ////{
        ////    _level.SetUpViewport(5, 1, 5, 5);
        ////    _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(7, 1));

        ////    // If player moves East or West, the viewport should shift East or West respectively
        ////    _level.MovePlayer(Direction.East);
        ////    Assert.That(6, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);
        ////    _level.MovePlayer(Direction.West);
        ////    Assert.That(5, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);

        ////    // If player Moves NorthEast Or NorthWest, the viewport just shifts East or West respectively
        ////    // It should not shift to the North as the player is still not located at the central row of the viewport
        ////    _level.MovePlayer(Direction.NorthEast);
        ////    Assert.That(6, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);
        ////    _level.MovePlayer(Direction.NorthWest);
        ////    Assert.That(5, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);
        ////}

        ////// Enough space on all sides to allow movement of MapOrigin
        ////// Test Viewport with even height and width
        ////[Test]
        ////public void Viewport_MovingPlayerWhenLocatedAtTheCenterOfAViewportWithEvenWidthAndHeight_MovesTheMapOriginCorrectly()
        ////{
        ////    _level.SetUpViewport(3, 4, 6, 6);
        ////    _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(6, 7));

        ////    foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        ////    {
        ////        _level.MovePlayer(direction);
        ////        Assert.That(3, Math.Abs(_level.Viewport.MapOrigin.X - _level.CharacterManager.Player.GetComponent<Position>().X));
        ////        Assert.That(3, Math.Abs(_level.Viewport.MapOrigin.Y - _level.CharacterManager.Player.GetComponent<Position>().Y));

        ////        // Reset viewport and player's position
        ////        _level.Viewport.MapOrigin.X = 3;
        ////        _level.Viewport.MapOrigin.Y = 4;
        ////        _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(6, 7));
        ////    }
        ////}

        ////// Test Viewport does not move when player hits obstacle or player hits character
        ////[Test]
        ////public void Viewport_WhenAMovingPlayerHitsAnObstacleOrCharacter_DoesNotMoveItself()
        ////{
        ////    _level.SetUpViewport(4, 1, 6, 6);
        ////    _level.MoveCharacterTo(_level.CharacterManager.Player, new Position(7, 4));

        ////    _level.MovePlayer(Direction.North);

        ////    Assert.That(4, _level.Viewport.MapOrigin.X);
        ////    Assert.That(1, _level.Viewport.MapOrigin.Y);
        ////}

        // Focusing a viewport
        [Test]
        public void FocusOn_GivenAPositionAnsAViewportWithEvenWidthAndHeightAndEnoughSpaceAroundFocusPoint_FocusesViewportAtPosition()
        {
            Viewport viewport = new Viewport(6, 6);

            viewport.FocusOn(new Position(5, 5));

            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(2, 2)));
        }

        [Test]
        public void FocusOn_GivenAPositionAndAViewportWithAnOddWidthAndHeightAndEnoughSpaceAroundFocusPoint_FocusesViewportAtPosition()
        {
            Viewport viewport = new Viewport(5, 5);

            viewport.FocusOn(new Position(5, 5));

            Assert.That(viewport.Bounds.BottomLeft, Is.EqualTo(new Position(3, 3)));
        }

        //[Test]
        //public void CenterAt_WhenThereIsNotEnoughSpaceAroundCenter_CentersViewportAsMuchAsPossible()
        //{
        //    _level.SetUpViewport(5, 5);

        //    // Bottom left
        //    _level.Viewport.CenterAt(new Position(0, 0));
        //    Assert.That(_level.Viewport.MapLocation, Is.EqualTo(new Position(0, 0)));

        //    // Bottom right
        //    _level.Viewport.CenterAt(new Position(_level.Map.Width - 1, 0));
        //    Assert.That(_level.Viewport.MapLocation.X, Is.EqualTo(_level.Map.Width - ((IBounded)_level.Viewport).Bounds.Width));
        //    Assert.That(_level.Viewport.MapLocation.Y, Is.EqualTo(0));

        //    // Top right
        //    _level.Viewport.CenterAt(new Position(_level.Map.Width - 1, _level.Map.Height - 1));
        //    Assert.That(_level.Viewport.MapLocation.X, Is.EqualTo(_level.Map.Width - ((IBounded)_level.Viewport).Bounds.Width));
        //    Assert.That(_level.Viewport.MapLocation.Y, Is.EqualTo(_level.Map.Height - ((IBounded)_level.Viewport).Bounds.Height));

        //    // Top left
        //    _level.Viewport.CenterAt(new Position(0, _level.Map.Height - 1));
        //    Assert.That(_level.Viewport.MapLocation.X, Is.EqualTo(0));
        //    Assert.That(_level.Viewport.MapLocation.Y, Is.EqualTo(_level.Map.Height - ((IBounded)_level.Viewport).Bounds.Height));
        //}
    }
}