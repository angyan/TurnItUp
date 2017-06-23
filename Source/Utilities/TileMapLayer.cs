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
        }
    }
}