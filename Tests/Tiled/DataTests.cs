using NUnit.Framework;
using System;
using System.IO;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void ParameterlessConstructorExists()
        {
            Data data = new Data();

            Assert.That(data.Value, Is.Null);
            Assert.That(data.Encoding, Is.EqualTo(Encoding.Base64));
            Assert.That(data.Compression, Is.EqualTo(Compression.Zlib));
        }
    }
}