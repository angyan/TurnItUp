using NUnit.Framework;
using Turnable.Characters;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Characters
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Constructor_GivenAPositionLevelAndTileId_InitializesThePlayer()
        {
            var level = new Level();
            var player = new Player(new Position(0, 0), level, 1);

            Assert.That(level.Player, Is.EqualTo(player));
            Assert.That(player.Level, Is.EqualTo(level));
            Assert.That(player.Position, Is.EqualTo(new Position(0, 0)));
            Assert.That(player.TileId, Is.EqualTo(1));
        }
    }
}