using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Turnable.Tiled
{
    [XmlType("tileset")]
    public class Tileset
    {
        [XmlAttribute("version")]
        public string Version { get; set; }
        [XmlAttribute("tiledversion")]
        public string TiledVersion { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }
        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }
        [XmlAttribute("tilecount")]
        public int TileCount { get; set; }
        [XmlAttribute("columns")]
        public int Columns { get; set; }
        [XmlElement("tile")]
        public List<Tile> Tiles { get; set; }

        public Tileset()
        {
        }

        public static Tileset Load(string fullPath)
        {
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Tileset));

            var tileset = (Tileset)serializer.Deserialize(fileStream);

            fileStream.Close();
            return tileset;
        }

        public uint? FindTileIdWithProperty(string propertyName, string propertyValue)
        {
            foreach (Tile tile in Tiles)
            {
                foreach (Property property in tile.Properties)
                {
                    if (property.Name == propertyName && property.Value == propertyValue)
                    {
                        return tile.Id;
                    }
                }
            }

            return null;
        }
    }
}
