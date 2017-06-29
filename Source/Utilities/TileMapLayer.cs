using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using Turnable.Tiled;

namespace Turnable.Utilities
{
    public class TileMapLayer : INamedElement
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<Position, uint> Tiles { get; }

        public TileMapLayer(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
            Tiles = new Dictionary<Position, uint>();
        }

        public TileMapLayer(string name, int width, int height, Data data) : this(name, width, height)
        {
            // Decode the data
            byte[] decodedData = Convert.FromBase64String((string)data.Value);
            Stream dataStream = new MemoryStream(decodedData, false);

            // Uncompress the decoded data
            switch (data.Compression)
            {
                // TODO: Move decompression to Data?
                // TODO: Unit test both Gzip and Zlib decompression?
                // case Compression.Gzip:
                //    dataStream = new GZipStream(dataStream, CompressionMode.Decompress, false);
                //    break;
                case Compression.Zlib:
                    dataStream = new ZlibStream(dataStream, CompressionMode.Decompress, false);
                    break;
            }

            // Read from the decoded and uncompressed stream
            uint tileGlobalId = 0;

            using (BinaryReader reader = new BinaryReader(dataStream))
            {
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        tileGlobalId = reader.ReadUInt32();

                        // The .tmx format uses 0 to indicate a tile that hasn't been set
                        if (tileGlobalId != 0)
                        {
                            Tiles[new Position(col, row)] = tileGlobalId;
                        }
                    }
                }
            }
        }

        public uint? GetTile(Position position)
        {
            if (!Tiles.ContainsKey(position))
            {
                return null;
            }

            return Tiles[position];
        }

        public void SetTile(Position position, uint tileGlobalId)
        {
            Tiles[position] = tileGlobalId;
        }

        public void SwapTile(Position position1, Position position2)
        {
            // Swapping two tiles requires tiles to be present at both positions.
            if (!Tiles.ContainsKey(position1) || !Tiles.ContainsKey(position2))
            {
                throw new InvalidOperationException();
            }

            uint tile = Tiles[position1];
            Tiles[position1] = Tiles[position2];
            Tiles[position2] = tile;
        }
    }
}