using Turnable.Tiled;

namespace Turnable.Utilities
{
    public class TileMap
    {
        public Map Map { get; set; }
        public NamedElementCollection<TileMapLayer> Layers { get; }

        public TileMap(string fullPath)
        {
            Layers = new NamedElementCollection<TileMapLayer>();
            Map = Map.Load(fullPath);

            foreach(Layer layer in Map.Layers)
            {
                Layers.Add(new TileMapLayer(layer.Name, layer.Width, layer.Height, layer.Data));
            }
        }

        public uint? GetTile(int x, int y, int layerIndex)
        {
            return Layers[layerIndex].GetTile(new Position(x, y));
        }
    }
}
