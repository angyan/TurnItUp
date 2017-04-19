using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Tests.Tiled
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Load_WhenGivenAFullPathToATmxFile_LoadsTheTiledMap()
        {
            Map map = Map.Load("");


        }
    }
}
