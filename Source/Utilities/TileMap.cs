using Turnable.Tiled;

namespace Turnable.Utilities
{
    public class TileMap
    {
        public Map Map { get; set; }
        public NamedElementCollection<TileMapLayer> TileMapLayers { get; }

        public TileMap(string fullPath)
        {
            TileMapLayers = new NamedElementCollection<TileMapLayer>();
            Map = Map.Load(fullPath);

            foreach(Layer layer in Map.Layers)
            {
                TileMapLayers.Add(new TileMapLayer(layer.Name, layer.Width, layer.Height, layer.Data));
            }
        }
    }
}
