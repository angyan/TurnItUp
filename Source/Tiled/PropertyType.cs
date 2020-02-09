using System.Xml.Serialization;

namespace Turnable.Tiled
{
    public enum PropertyType
    {
        [XmlEnum(Name = "string")]
        String = 0,
        [XmlEnum(Name = "int")]
        Int,
        [XmlEnum(Name = "float")]
        Float,
        [XmlEnum(Name = "bool")]
        Bool,
        [XmlEnum(Name = "color")]
        Color,
        [XmlEnum(Name = "file")]
        File
    }
}
