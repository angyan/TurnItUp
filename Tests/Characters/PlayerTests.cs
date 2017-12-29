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
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");
            var level = new Level(fullPath);

            var player = new Player(new Position(0, 0), level, 1);

            Assert.That(level.Player, Is.EqualTo(player));
            Assert.That(player.Level, Is.EqualTo(level));
            Assert.That(player.Position, Is.EqualTo(new Position(0, 0)));
            uint tileId = level.TileMap.Layers["Characters"].Tiles[new Position(0, 0)];
            Assert.That(tileId, Is.EqualTo(1));
        }
    }
}