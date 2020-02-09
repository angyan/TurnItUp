using NUnit.Framework;
using System;
using System.IO;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class TileTests
    {
        [Test]
        public void ParameterlessConstructor_Exists()
        {
            var tile = new Tile();
            
            Assert.That(tile.Id, Is.Zero);
        }
    }
}
