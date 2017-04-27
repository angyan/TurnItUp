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

            // Are map attributes correctly set?
            Assert.That(map, Is.Not.Null);
            Assert.That(map.Version, Is.EqualTo("1.0"));
            Assert.That(map.Orientation, Is.EqualTo(Orientation.Orthogonal));
            Assert.That(map.RenderOrder, Is.EqualTo(RenderOrder.RightDown));
            Assert.That(map.Width, Is.EqualTo(45));
            Assert.That(map.Height, Is.EqualTo(31));
            Assert.That(map.TileWidth, Is.EqualTo(16));
            Assert.That(map.TileHeight, Is.EqualTo(16));
            Assert.That(map.NextObjectId, Is.EqualTo(37));
            // Are the properties for the map correctly set?
            Assert.That(map.Properties, Is.Not.Null);
        }
    }
}
