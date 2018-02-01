using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnable.Utilities;

namespace Turnable.Pathfinding
{
    public class Pathfinder
    {
        public bool AllowDiagonalMovement { get; set; }
        public Rectangle Bounds { get; set; }

        public Pathfinder(Rectangle bounds, bool allowDiagonalMovement = true)
        {
            Bounds = bounds;
            AllowDiagonalMovement = allowDiagonalMovement;
        }

        public List<Position> FindPath(Position start, Position end, IEnumerable<Position> obstacles)
        {
            // If the end is an obstacle, there is no path that can be found. 
            // This is an exceptional situation, since most games should never even consider finding a path to an obstacle.
            if (obstacles.Contains<Position>(end))
            {
                throw new InvalidOperationException();
            }

            var returnValue = new List<Position>();

            returnValue.Add(start);
            returnValue.Add(end);

            return returnValue;
        }
    }
}
