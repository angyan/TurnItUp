using System;
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

            Position newPlayerPosition = Player.Position.NeighboringPosition(direction);

            if (TileMap.Bounds.Contains(newPlayerPosition))
            {
                returnValue.Status = MovementStatus.Success;
                returnValue.Path.Add(Player.Position);
                returnValue.Path.Add(newPlayerPosition);
            }
            else
            {
                returnValue.Status = MovementStatus.OutOfBounds;
            }

            return returnValue;
        }
    }
}
