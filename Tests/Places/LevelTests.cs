using NUnit.Framework;
using System;
using System.IO;
using Turnable.Characters;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Places
{
    [TestFixture]
    public class LevelTests
    {
        [Test]
        public void ParameterlessConstructorExists()
        {
            var level = new Level();

            Assert.That(level.TileMap, Is.Null);
            Assert.That(level.Viewport, Is.Null);
            Assert.That(level.Player, Is.Null);
        }

        [Test]
        public void Constructor_GivenAFullPathToATmxFile_InitializesLevelWithATileMapAndDefaultViewport()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");

            var level = new Level(fullPath);

            // Was the TileMap loaded?
            Assert.That(level.TileMap, Is.Not.Null);
            // Was an extra TileMapLayer added to hold the characters?
            Assert.That(level.TileMap.Layers.Count, Is.EqualTo(level.TileMap.Map.Layers.Count + 1));
            Assert.That(level.TileMap.Layers["Characters"], Is.Not.Null);
            foreach (TileMapLayer tileMapLayer in level.TileMap.Layers)
            {
                if (tileMapLayer.Name != "Characters")
                {
                    Assert.That(tileMapLayer.Tiles.Count, Is.Not.Zero);
                }
            }
            // Was a default viewport initialized?
            Assert.That(level.Viewport, Is.Not.Null);
            Assert.That(level.Viewport.Bounds.Width, Is.EqualTo(16));
            Assert.That(level.Viewport.Bounds.Height, Is.EqualTo(16));
            Assert.That(level.Viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
        }

        // -----------------
        // Move player tests
        // -----------------
        [Test]
        public void MovePlayerInDirection_GivenADirectionWithNoObstacles_ReturnsAMovement()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");
            var level = new Level(fullPath);
            var player = new Player(new Position(0, 0), level, 1);

            Movement movement = level.MovePlayerInDirection(Direction.North);

            // Is all the information returned correct in the movement?
            var newPlayerPosition = new Position(0, 0).NeighboringPosition(Direction.North);
            Assert.That(movement.Status, Is.EqualTo(MovementStatus.Success));
            Assert.That(movement.Path.Count, Is.EqualTo(2));
            Assert.That(movement.Path[0], Is.EqualTo(new Position(0, 0)));
            Assert.That(movement.Path[1], Is.EqualTo(newPlayerPosition));
            // Was the player moved alongwith the relevant tile on the TileMap?
            Assert.That(level.Player.Position, Is.EqualTo(newPlayerPosition));
            Assert.That(level.TileMap.Layers["Characters"].GetTile(new Position(0, 0)), Is.Null);
            Assert.That(level.TileMap.Layers["Characters"].GetTile(newPlayerPosition), Is.EqualTo(1));
        }

        [Test]
        public void MovePlayerInDirection_GivenADirectionWhichWouldMoveThePlayerOutOfBounds_ReturnsAMovement()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");
            var level = new Level(fullPath);
            var player = new Player(new Position(0, 0), level, 1);

            Movement movement = level.MovePlayerInDirection(Direction.South);

            Assert.That(movement.Status, Is.EqualTo(MovementStatus.OutOfBounds));
            Assert.That(movement.Path.Count, Is.EqualTo(0));
            // Was the player left in the same position? Was there no change to the player tile on the TileMap?
            Assert.That(level.Player.Position, Is.EqualTo(new Position(0, 0)));
        }
    }
}