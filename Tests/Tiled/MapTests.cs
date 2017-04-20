using NUnit.Framework;
using Turnable.Tiled;

namespace Tests.Tiled
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Load_WhenGivenAFullPathToATmxFile_LoadsTheTiledMap()
        {
            Map map = Map.Load("");

            Assert.IsNotNull(map);

        }
    }
}
