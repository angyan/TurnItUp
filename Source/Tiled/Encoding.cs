using System.Xml.Serialization;

namespace Turnable.Tiled
{
    public enum Encoding
    {
        [XmlEnum(Name = "base64")]
        Base64 = 0,
        [XmlEnum(Name = "csv")]
        Csv
    }
}
