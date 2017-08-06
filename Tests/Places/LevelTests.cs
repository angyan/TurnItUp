using NUnit.Framework;
using System;
using System.IO;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Places
{
    [TestFixture]
    public class LevelTests
    {
        [Test]
        public void Constructor_GivenAFullPathToATmxFile_InitializesATileMap()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");

            Level level = new Level(fullPath);

            // Was the TileMap loaded?
            Assert.That(level.TileMap, Is.Not.Null);
            Assert.That(level.TileMap.Layers.Count, Is.EqualTo(level.TileMap.Map.Layers.Count));
            foreach (TileMapLayer tileMapLayer in level.TileMap.Layers)
            {
                Assert.That(tileMapLayer.Tiles.Count, Is.Not.Zero);
            }
            // Was a default viewport initialized?
            Assert.That(level.Viewport, Is.Not.Null);
            Assert.That(level.Viewport.Bounds.Width, Is.EqualTo(16));
            Assert.That(level.Viewport.Bounds.Height, Is.EqualTo(16));
            Assert.That(level.Viewport.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
        }
    }
}