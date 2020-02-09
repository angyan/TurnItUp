using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turnable.Places
{
    public enum MovementStatus
    {
        None,
        Success,
        HitObstacle,
        HitCharacter,
        OutOfBounds
    }
}