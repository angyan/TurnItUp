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
        private string value;

        [XmlText]
        public string Value
        {
            get { return value; }
            set { this.value = value.Trim(); }
        }
        public Encoding Encoding { get; set; }
        public Compression Compression { get; set; }
    }
}
