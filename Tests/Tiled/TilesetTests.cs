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
        public void ParameterlessConstructor_Exists()
        {
            var tileset = new Tileset();

            Assert.That(tileset.FirstGlobalTileId, Is.EqualTo(0));
            Assert.That(tileset.Name, Is.Null);
            Assert.That(tileset.TileWidth, Is.Zero);
            Assert.That(tileset.TileHeight, Is.Zero);
            Assert.That(tileset.Columns, Is.Zero);
        }
    }
}
