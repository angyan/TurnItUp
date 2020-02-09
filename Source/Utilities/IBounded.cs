using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turnable.Utilities
{
    public interface IBounded
    {
        Rectangle Bounds { get; set; }
    }
}
