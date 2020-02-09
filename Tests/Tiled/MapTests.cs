using NUnit.Framework;
using System;
using System.IO;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Load_GivenAFullPathToATmxFile_LoadsTheTiledMap()
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\simple.tmx");

            var map = Map.Load(fullPath);

            // Are the map attributes correctly set?
            Assert.That(map, Is.Not.Null);
            Assert.That(map.Version, Is.EqualTo("1.2"));
            Assert.That(map.Orientation, Is.EqualTo(Orientation.Orthogonal));
            Assert.That(map.RenderOrder, Is.EqualTo(RenderOrder.RightDown));
            Assert.That(map.Width, Is.EqualTo(16));
            Assert.That(map.Height, Is.EqualTo(16));
            Assert.That(map.TileWidth, Is.EqualTo(48));
            Assert.That(map.TileHeight, Is.EqualTo(48));
            Assert.That(map.NextObjectId, Is.EqualTo(1));
            // Are the layers correctly loaded?
            Assert.That(map.Layers, Is.Not.Null);
            Assert.That(map.Layers.Count, Is.EqualTo(1));
            Layer layer = map.Layers[0];
            Assert.That(layer.Name, Is.EqualTo("Background"));
            Assert.That(layer.Width, Is.EqualTo(16));
            Assert.That(layer.Height, Is.EqualTo(16));
            // Are the tilesets correctly loaded?
            Assert.That(map.Tilesets, Is.Not.Null);
            Assert.That(map.Tilesets.Count, Is.EqualTo(1));
            Tileset tileset = map.Tilesets[0];
            Assert.That(tileset.Name, Is.Null);
            Assert.That(tileset.TileWidth, Is.Zero);
            Assert.That(tileset.TileHeight, Is.Zero);
            Assert.That(tileset.Columns, Is.Zero);
            // Was the data for each layer correctly loaded?
            Assert.That(layer.Data, Is.Not.Null);
            Assert.That(layer.Data.Encoding, Is.EqualTo(Encoding.Base64));
            Assert.That(layer.Data.Compression, Is.EqualTo(Compression.Zlib));
            Assert.That(layer.Data.Value, Is.EqualTo("eJzzZGBg8BzFo3gUj0gMAJ6OSQE="));
        }
    }
}
