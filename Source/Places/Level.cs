using Turnable.Characters;
using Turnable.Utilities;

namespace Turnable.Places
{
    public class Level
    {
        public TileMap TileMap { get; set; }
        public Viewport Viewport { get; set; }
        public Player Player { get; set; }

        public Level()
        {
            TileMap = null;
            Viewport = null;
        }

        public Level(string fullPath) : this()
        {
            TileMap = new TileMap(fullPath);
            Viewport = new Viewport(16, 16);
        }

        public Movement MovePlayerInDirection(Direction direction)
        {
            Movement returnValue = new Movement();

            returnValue.Status = MovementStatus.Success;
            return returnValue;
        }
    }
}
