using System;
using System.IO;
using NUnit.Framework;
using Turnable.Utilities;

namespace Tests.Tiled
{
    [TestFixture]
    public class TileMapTests
    {
        private TileMap tileMap;

        [Test]
        public void Constructor_GivenAFullPathToATmxFile_LoadsATiledMapAndTheTilesPerLayer()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");

            tileMap = new TileMap(fullPath);

            // Was the Tiled map loaded?
            Assert.That(tileMap.Map, Is.Not.Null);
            Assert.That(tileMap.Layers.Count, Is.EqualTo(tileMap.Map.Layers.Count));
            foreach(TileMapLayer tileMapLayer in tileMap.Layers)
            {
                Assert.That(tileMapLayer.Tiles.Count, Is.Not.Zero);
            }
        }

        [Test]
        public void GetTile_GivenXYAndLayerIndex_ReturnsATile()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");
            tileMap = new TileMap(fullPath);

            Assert.That(tileMap.GetTile(0, 0, 0), Is.EqualTo(223));

        }

    }
}
