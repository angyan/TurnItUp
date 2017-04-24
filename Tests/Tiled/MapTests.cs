using System;
using System.IO;
using NUnit.Framework;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Load_GivenAFullPathToATmxFile_LoadsTheTiledMap()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Fixtures\orthogonal-outside.tmx");
            Console.WriteLine(fullPath);
            var map = Map.Load(fullPath);

            Assert.IsNotNull(map);
            Assert.AreEqual("1.0", map.Version);
            Assert.AreEqual(Orientation.Orthogonal, map.Orientation);
            Assert.AreEqual(RenderOrder.RightDown, map.RenderOrder);
            Assert.AreEqual(45, map.Width);
            Assert.AreEqual(31, map.Height);
            Assert.AreEqual(16, map.TileWidth);
            Assert.AreEqual(16, map.TileHeight);
            Assert.AreEqual(37, map.NextObjectId);
        }
    }
}
