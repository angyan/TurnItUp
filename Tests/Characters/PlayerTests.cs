using NUnit.Framework;
using System;
using System.IO;
using Turnable.Characters;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Characters
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Constructor_GivenAPositionLevelAndTileId_InitializesThePlayer()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\simple-player.tmx");
            var level = new Level(fullPath);

            var player = new Player(new Position(1, 1), level, 1);

            Assert.That(level.Player, Is.EqualTo(player));
            Assert.That(player.Level, Is.EqualTo(level));
            Assert.That(player.Position, Is.EqualTo(new Position(1, 1)));
            uint tileId = level.TileMap.Layers["Characters"].Tiles[new Position(1, 1)];
            Assert.That(tileId, Is.EqualTo(1));
        }

        [Test]
        public void ConstructorForALevel_GivenATiledMapWhichAlreadyHasAPredefinedPlayer_InitializesThePlayer()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\simple-player.tmx");
            var level = new Level(fullPath);

            Assert.That(level.Player, Is.Not.Null);
            Assert.That(level.Player.Level, Is.EqualTo(level));
            Assert.That(level.Player.Position, Is.EqualTo(new Position(0, 0)));
            uint tileId = level.TileMap.Layers["Characters"].Tiles[new Position(1, 1)];
            Assert.That(tileId, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_GivenAPositionLevelAndTileId_OverridesThePlayerDefinedInTheLevel()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\simple-player.tmx");
            var level = new Level(fullPath);

            Assert.That(level.Player, Is.Not.Null);
            Assert.That(level.Player.Level, Is.EqualTo(level));
            Assert.That(level.Player.Position, Is.EqualTo(new Position(0, 0)));
            uint tileId = level.TileMap.Layers["Characters"].Tiles[new Position(1, 1)];
            Assert.That(tileId, Is.EqualTo(1));

            var player = new Player(new Position(1, 1), level, 1);

            Assert.That(level.Player, Is.EqualTo(player));
            Assert.That(player.Level, Is.EqualTo(level));
            Assert.That(player.Position, Is.EqualTo(new Position(1, 1)));
            tileId = level.TileMap.Layers["Characters"].Tiles[new Position(1, 1)];
            Assert.That(tileId, Is.EqualTo(1));
        }
    }
}