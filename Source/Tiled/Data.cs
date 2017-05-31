using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    public class Data
    {
        private string originalContents;

        [XmlText]
        public string OriginalContents
        {
            get { return originalContents; }
            set
            {
                originalContents = value.Trim();
                byte[] rawContents = Convert.FromBase64String(OriginalContents);
                Contents = new Ionic.Zlib.ZlibStream(new MemoryStream(rawContents, false), Ionic.Zlib.CompressionMode.Decompress, false);
            }
        }
        public Encoding Encoding { get; set; }
        public Compression Compression { get; set; }
        [XmlIgnore]
        public Stream Contents { get; private set; }
    }
}
