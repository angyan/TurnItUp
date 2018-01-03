using System;
using Turnable.Tiled;

namespace Turnable.Utilities
{
    public class TileMap : IBounded
    {
        public Map Map { get; set; }
        public NamedElementCollection<TileMapLayer> Layers { get; }
        public Rectangle Bounds { get; set; }

        public TileMap(string fullPath)
        {
            Layers = new NamedElementCollection<TileMapLayer>();
            Map = Map.Load(fullPath);

            foreach(Layer layer in Map.Layers)
            {
                Layers.Add(new TileMapLayer(layer.Name, layer.Width, layer.Height, layer.Data));
            }
            Bounds = new Rectangle(new Position(0, 0), Map.Width, Map.Height);
        }

        public uint? GetTile(int x, int y, int layerIndex)
        {
            return GetTile(x, y, Layers[layerIndex]);
        }

        public uint? GetTile(int x, int y, TileMapLayer layer)
        {
            return layer.GetTile(new Position(x, y));
        }
    }
}
