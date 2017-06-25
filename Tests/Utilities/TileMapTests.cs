using System;
using System.IO;
using NUnit.Framework;
using Turnable.Tiled;
using System.Xml.Linq;
using Turnable.Utilities;
using NCrunch.Framework;

namespace Tests.Tiled
{
    [TestFixture]
    public class TileMapTests
    {
        [Test]
        public void Constructor_GivenAFullPathToATmxFile_LoadsATiledMapAndTheTilesPerLayer()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Fixtures\orthogonal-outside.tmx");

            TileMap tileMap = new TileMap(fullPath);

            // Was the Tiled map loaded?
            Assert.That(tileMap.Map, Is.Not.Null);
            //Assert.That(tileMap.TileMapLayers.Count, Is.EqualTo(tileMap.Map.Layers.Count));
        }
    }
}
