using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    public enum Compression
    {
        [XmlEnum(Name = "zlib")]
        Zlib = 0,
        [XmlEnum(Name = "gzip")]
        Gzip
    }
}
