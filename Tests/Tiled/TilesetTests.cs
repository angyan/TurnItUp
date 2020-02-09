using NUnit.Framework;
using System;
using System.IO;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class TilesetTests
    {
        [Test]
        public void ParameterlessConstructor_Exists()
        {
            var tileset = new Tileset();
            
            Assert.That(tileset.Version, Is.Null);
            Assert.That(tileset.TiledVersion, Is.Null);
            Assert.That(tileset.Name, Is.Null);
            Assert.That(tileset.TileWidth, Is.Zero);
            Assert.That(tileset.TileHeight, Is.Zero);
            Assert.That(tileset.TileCount, Is.Zero);
            Assert.That(tileset.Columns, Is.Zero);
        }

        [Test]
        public void Constructor_GivenAFullPathToATsxFile_InitializesTheTileset()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\Characters.tsx");

            var tileset = Tileset.Load(fullPath);

            Assert.That(tileset.Version, Is.EqualTo("1.2"));
            Assert.That(tileset.TiledVersion, Is.EqualTo("1.2.1"));
            Assert.That(tileset.Name, Is.EqualTo("Characters"));
            Assert.That(tileset.TileWidth, Is.EqualTo(48));
            Assert.That(tileset.TileHeight, Is.EqualTo(48));
            Assert.That(tileset.TileCount, Is.EqualTo(520));
            Assert.That(tileset.Columns, Is.EqualTo(40));

            // Have tiles (with custom properties) been loaded?
            Assert.That(tileset.Tiles, Is.Not.Null);
            Assert.That(tileset.Tiles.Count, Is.EqualTo(1));
            Assert.That(tileset.Tiles[0].Id, Is.Zero);
            Console.WriteLine(tileset.Tiles[0].Properties.Count);
            Property property = tileset.Tiles[0].Properties[0];
            Assert.That(property.Name, Is.EqualTo("Player"));
            Assert.That(property.Value, Is.EqualTo("true"));
            Assert.That(property.Type, Is.EqualTo(PropertyType.Bool));
        }
    }
}
