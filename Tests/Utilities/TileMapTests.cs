using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Turnable.Utilities;

namespace Tests.Tiled
{
    [TestFixture]
    public class TileMapTests
    {
        private TileMap tileMap;

        [SetUp]
        public void SetUp()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\orthogonal-outside.tmx");
            tileMap = new TileMap(fullPath);
        }

        [Test]
        public void Constructor_GivenAFullPathToATmxFile_InitializesTheTileMap()
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
            // Was the bounds of the TileMap correctly set?
            Assert.That(tileMap.Bounds.BottomLeft, Is.EqualTo(new Position(0, 0)));
            Assert.That(tileMap.Bounds.Width, Is.EqualTo(tileMap.Layers[0].Width));
            Assert.That(tileMap.Bounds.Height, Is.EqualTo(tileMap.Layers[0].Height));
        }

        [Test]
        public void TileMap_ImplementsTheIBoundedInterface()
        {
            Assert.That(tileMap, Is.InstanceOf<IBounded>());
        }

        [Test]
        public void GetTile_GivenXYAndLayerIndex_ReturnsATile()
        {
            Assert.That(tileMap.GetTile(0, 0, 0), Is.EqualTo(223));
        }

        [Test]
        public void GetTile_GivenXYAndLayer_ReturnsATile()
        {
            Assert.That(tileMap.GetTile(0, 0, tileMap.Layers[0]), Is.EqualTo(223));
        }

        [Test]
        public void FindTileIdWithProperty_GivenAPropertyNameAndValue_FindsTileIdWithThatPropoertySet()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\Characters.tsx");

            var tileset = Tileset.Load(fullPath);

            uint? foundTileId = tileset.FindTileIdWithProperty("Player", "true");

            Assert.That(foundTileId, Is.Zero);
        }

        [Test]
        public void FindTileIdWithProperty_GivenAPropertyNameAndValueThatDoesNotExist_ReturnsNull()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\Characters.tsx");

            var tileset = Tileset.Load(fullPath);

            uint? foundTileId = tileset.FindTileIdWithProperty("MissingProperty", "true");

            Assert.That(foundTileId, Is.Null);
        }


        [Test]
        public void GetTilePositions_GivenATileIdAndLayerIndex_ReturnsAllPositionsWithThatTile()
        {
            List<Position> tilePositions = tileMap.GetTilePositions(0, 1);

            Assert.That(tilePositions.Count, Is.EqualTo(1));

        }
    }
}
