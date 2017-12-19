using System.Collections.Generic;
using Turnable.Utilities;

namespace Turnable.Places
{
    public class Movement
    {
        public MovementStatus Status { get; set; }
        public List<Position> Path { get; set; }
    }
}