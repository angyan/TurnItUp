using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turnable.Tiled
{
    public class Property
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public PropertyType Type { get; set; }

        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Property(string name, string value, PropertyType type) : this(name, value)
        {
            Type = type;
        }
    }
}
