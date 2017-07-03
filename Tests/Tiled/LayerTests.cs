using NUnit.Framework;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class LayerTests
    {
        [Test]
        public void ParameterlessConstructor_Exists()
        {
            var layer = new Layer();

            Assert.That(layer.Name, Is.Null);
            Assert.That(layer.Width, Is.Zero);
            Assert.That(layer.Height, Is.Zero);
        }
    }
}
