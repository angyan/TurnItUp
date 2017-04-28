using System;
using System.IO;
using NUnit.Framework;
using Turnable.Tiled;
using System.Xml.Linq;

namespace Tests.Tiled
{
    [TestFixture]
    public class TilesetTests
    {
        [Test]
        public void Constructor_GivenATilesetXElement_InitializesTheTileset()
        {
            var tilesetXElement = new XElement("property", new XAttribute("firstgid", "1"), new XAttribute("name", "outdoor"), new XAttribute("tilewidth", "16"), new XAttribute("tileheight", "16"), new XAttribute("columns", "24"));

            var tileset = new Tileset(tilesetXElement);

            Assert.That(tileset.FirstGlobalTileId, Is.EqualTo(1));
            Assert.That(tileset.Name, Is.EqualTo("outdoor"));
            Assert.That(tileset.TileWidth, Is.EqualTo(16));
            Assert.That(tileset.TileHeight, Is.EqualTo(16));
            Assert.That(tileset.Columns, Is.EqualTo(24));
        }
    }
}
