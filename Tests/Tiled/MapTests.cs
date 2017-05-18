using System;
using System.IO;
using NUnit.Framework;
using Turnable.Tiled;
using System.Linq;

namespace Tests.Tiled
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Load_GivenAFullPathToATmxFile_LoadsTheTiledMap()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Fixtures\orthogonal-outside.tmx");
            Console.WriteLine(fullPath);

            var map = Map.Load(fullPath);

            // Are the map attributes correctly set?
            Assert.That(map, Is.Not.Null);
            Assert.That(map.Version, Is.EqualTo("1.0"));
            Assert.That(map.Orientation, Is.EqualTo(Orientation.Orthogonal));
            Assert.That(map.RenderOrder, Is.EqualTo(RenderOrder.RightDown));
            Assert.That(map.Width, Is.EqualTo(45));
            Assert.That(map.Height, Is.EqualTo(31));
            Assert.That(map.TileWidth, Is.EqualTo(16));
            Assert.That(map.TileHeight, Is.EqualTo(16));
            Assert.That(map.NextObjectId, Is.EqualTo(37));
            // Are the properties for the map correctly set?
            Assert.That(map.Properties, Is.Not.Null);
            Assert.That(map.Properties.Count, Is.EqualTo(1));
            Property property = map.Properties[0];
            Assert.That(property.Name, Is.EqualTo("enemyTint"));
            Assert.That(property.Value, Is.EqualTo("#ffa33636"));
            Assert.That(property.Type, Is.EqualTo(PropertyType.Color));
            // Are the tilesets correctly loaded?
            Assert.That(map.Tilesets, Is.Not.Null);
            Assert.That(map.Tilesets.Count, Is.EqualTo(1));
            Tileset tileset = map.Tilesets[0];
            Assert.That(tileset.FirstGlobalTileId, Is.EqualTo(1));
            Assert.That(tileset.Name, Is.EqualTo("outdoor"));
            Assert.That(tileset.TileWidth, Is.EqualTo(16));
            Assert.That(tileset.TileHeight, Is.EqualTo(16));
            Assert.That(tileset.Columns, Is.EqualTo(24));
            // Are the layers correctly loaded?
            Assert.That(map.Layers, Is.Not.Null);
            Assert.That(map.Layers.Count, Is.EqualTo(2));
            Layer layer = map.Layers[0];
            Assert.That(layer.Name, Is.EqualTo("Ground"));
            Assert.That(layer.Width, Is.EqualTo(45));
            Assert.That(layer.Height, Is.EqualTo(31));
            // Was the data for each layer correctly loaded?
            Assert.That(layer.Data, Is.Not.Null);
            Assert.That(layer.Data.Encoding, Is.EqualTo(Encoding.Base64));
            Assert.That(layer.Data.Compression, Is.EqualTo(Compression.Zlib));
            Assert.That(layer.Data.OriginalContents, Is.EqualTo("eJyNWE1vVVUU3Y0KQeXL4kBL7QAiEkcopQOIkjgysXagYeLIpE0HGucEtY5U6giw1Bj8AdiWxhZ+ANLS4nv+AJKW1+TVH9DklWfSR+LeeWt51j3eWxmsnPvuPR9rr7P3Pvu8hpkd7DFb8dYb24Pf89bFtOOio8/xquNreZ/jdccxx0nH53j+SN7F81nHOfz+BGs1vP0C706i/1kZ+6asMWRdrvMY+4q3bzmedTwj/FfAO/i2HVcc10s4f+t4QzidcEw6vnd8lz0HfrAu3wOO8xj3seO44AT6cY0GONPWTx1jjp8cvzjO4Fsbfan/luP3XXgrJ+4XtWnjeQX9VzBmErgsdud8lXdwCb94G5w3HE+g+R5ZcwDPDx114U3dY569wkW5UZ8J8I6+AfalPbT7MvhS03nMMV3CNzT+A5zHMt70obvezjrWwfue47HwynUhH3JivwG01E1Be0cs8eW3mOd9x2lwXHVsOx7AP8bgI/PCZ5+PmwHvHceSaK76UZO2cGyg1X2mT/eKrtOwUX2Le/RcicbB4zfHyz6239sPs3VijtB6E33XRPPr0CbXmpwaAvrEPPyBvsDvOn4IbQ+4jkHnDaxfE52POl6zFIe0n1oH12Vw37Hq2HzH8W5o6DgE9FrRn+l7uk8rVvSZi6LvDdF4GaBPU2edS32kDjub1vWrMt6Mq8gJkc/OwQbypq9T54jrKcfVkrl+Fq5ctwn9/hTOZefIgPC+7VhEW9tFb+bwyL2Rw3le5DHGeVuY6xrma0DPJtpYL+Ip/JT+TM7uT9/k649jji3MEWMO+5gF2Py4hLOel8y/A1bMKdRbeW8K99BkzlIOmMVe37GuL2u+y/PYCNbbAkfGwCo0vyX9cq0nwbcsXnP/e97xAjADzutoQ6def3+kJ2lMnCmZL/wl/C7y87bsTXDuOB5Bt//jpbgiflBlQwe6am4bE40Dw6J1PscW9inmiJhlvvzVcaAn7fnT8JyC/ZG77u/SX3Nx8CPnQNRxl6xY05XZPYN5HoBv+F+VL49kccF9on9GG757q2J84KXMD0bBrw/+EHt7yro5umqOh7IW1330FHtNvkvQdg72co8GKtajnn2ib+QJ1p7T2XMgzlDPI//mv+C8Db5NWVO/MeangB/Bt4Y9CnuXs7FVIEfWofy9d5cxPF8mRKtNaDQLvxiy5O/xvgMteQ5si7Y1PLdkf8owjvW+hIan7L85gnXJRLd/wQ+ZD+jPjOU1K9ao1JLfeWatgfdtvGcNsBtnzjcu+/+VdWMubNA6i3lNx9MWahl7qvUHa1RqS79pWjord9C/hedFjLsKaByQ72KJLax1437GmqZX+OV2Mz+FP9y0dI52wKuFd3y/CH3Zh1qz1f7x7n62X/USHm3ggqU7pdYyeZ57EedTxI3W1XXw25T1a+DFs4vn/Bz4d7BHm5bOOe4XvzdLeBBRO0YdwzszeYevjFvymYg1xiv1qFvKtx1o17QUi1EXMHYOgfsR8G9Z8hXWiZyrjvFVnFl/kfOxjDdrmeAZeeuapTOce8x6gHquAVEv6jnL51VozH6shdbAn3VnrBvxdtCS7/LdeUv/NUQNxntlG7ypK/dxHWuw/tbzlTVIrMlaKu4Zeu4egm30I+a/WbTL+KaaHoeWh8GX+sZ/I1HnDlixFr9nqY5dtFQztjK+vMurbsH7hqU7J/X+GxyXMGcNWJI9oy9csOJ/PKEr/0cJe96Dvjdl/3g/2ZZ1uH9/Wbe2GnR8AC7Rb8dSPOV2jaIfc7ae6ew7akX/nQRP+kC8j/OO98vgvA95Yj/q2v2SK7Sm5fx5rC2gT/R/YsXacRT7EeMXLMVdcNa4rYrByMd6B676j44+3QFn1U7jTLkzz6nGw/I93m9Yyot5HcdzhPkrELkrYjD8mnGZc2a9pnUO+Q5mPEO/fivG21Grtktty+8eqjM58b873tk+s25c6t0+cNdS/cAYv1Oxfhn6pR20oh+dzvoOZ/b+A6OnAUo="));
        }
    }
}
