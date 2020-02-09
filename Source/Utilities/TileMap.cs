﻿using System;
using System.Collections.Generic;
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

        public List<Position> GetTilePositions(uint tileId, int layerIndex)
        {
            List<Position> tilePositions = new List<Position>();

            for (int col = 0; col < Bounds.Width; col++)
            {
                for (int row = 0; row < Bounds.Height; row++)
                {
                    Console.Write(GetTile(col, row, layerIndex));
                    if (GetTile(col, row, layerIndex) == tileId)
                    {
                        tilePositions.Add(new Position(col, row));
                    }
                }
            }

            return tilePositions;
        }
    }
}
