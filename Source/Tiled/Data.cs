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

        public Data()
        {
        }

        public Data(string value) : this()
        {
            Value = value;
        }
    }
}
