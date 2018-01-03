using NUnit.Framework;
using Turnable.Tiled;
using Turnable.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace Tests.Tiled
{
    [TestFixture]
    public class TileMapLayerTests
    {
        private TileMapLayer tileMapLayer;

        [SetUp]
        public void SetUp()
        {
            tileMapLayer = new TileMapLayer("Name", 5, 10);
        }

        [Test]
        public void Constructor_GivenANameWidthAndHeight_InitializesAnEmptyTileMapLayer()
        {
            TileMapLayer tileMapLayer = new TileMapLayer("Name", 5, 10);

            Assert.That(tileMapLayer, Is.InstanceOf<INamedElement>());
            Assert.That(tileMapLayer.Name, Is.EqualTo("Name"));
            Assert.That(tileMapLayer.Width, Is.EqualTo(5));
            Assert.That(tileMapLayer.Height, Is.EqualTo(10));
            Assert.That(tileMapLayer.Tiles, Is.Not.Null);
            Assert.That(tileMapLayer.Tiles.Count, Is.Zero);
        }

        [Test]
        public void Constructor_GivenANameWidthHeightAndATiledLayer_InitializesATileMapLayerWithTiles()
        {
            Data data = new Data("eJyNWE1vVVUU3Y0KQeXL4kBL7QAiEkcopQOIkjgysXagYeLIpE0HGucEtY5U6giw1Bj8AdiWxhZ+ANLS4nv+AJKW1+TVH9DklWfSR+LeeWt51j3eWxmsnPvuPR9rr7P3Pvu8hpkd7DFb8dYb24Pf89bFtOOio8/xquNreZ/jdccxx0nH53j+SN7F81nHOfz+BGs1vP0C706i/1kZ+6asMWRdrvMY+4q3bzmedTwj/FfAO/i2HVcc10s4f+t4QzidcEw6vnd8lz0HfrAu3wOO8xj3seO44AT6cY0GONPWTx1jjp8cvzjO4Fsbfan/luP3XXgrJ+4XtWnjeQX9VzBmErgsdud8lXdwCb94G5w3HE+g+R5ZcwDPDx114U3dY569wkW5UZ8J8I6+AfalPbT7MvhS03nMMV3CNzT+A5zHMt70obvezjrWwfue47HwynUhH3JivwG01E1Be0cs8eW3mOd9x2lwXHVsOx7AP8bgI/PCZ5+PmwHvHceSaK76UZO2cGyg1X2mT/eKrtOwUX2Le/RcicbB4zfHyz6239sPs3VijtB6E33XRPPr0CbXmpwaAvrEPPyBvsDvOn4IbQ+4jkHnDaxfE52POl6zFIe0n1oH12Vw37Hq2HzH8W5o6DgE9FrRn+l7uk8rVvSZi6LvDdF4GaBPU2edS32kDjub1vWrMt6Mq8gJkc/OwQbypq9T54jrKcfVkrl+Fq5ctwn9/hTOZefIgPC+7VhEW9tFb+bwyL2Rw3le5DHGeVuY6xrma0DPJtpYL+Ip/JT+TM7uT9/k649jji3MEWMO+5gF2Py4hLOel8y/A1bMKdRbeW8K99BkzlIOmMVe37GuL2u+y/PYCNbbAkfGwCo0vyX9cq0nwbcsXnP/e97xAjADzutoQ6def3+kJ2lMnCmZL/wl/C7y87bsTXDuOB5Bt//jpbgiflBlQwe6am4bE40Dw6J1PscW9inmiJhlvvzVcaAn7fnT8JyC/ZG77u/SX3Nx8CPnQNRxl6xY05XZPYN5HoBv+F+VL49kccF9on9GG757q2J84KXMD0bBrw/+EHt7yro5umqOh7IW1330FHtNvkvQdg72co8GKtajnn2ib+QJ1p7T2XMgzlDPI//mv+C8Db5NWVO/MeangB/Bt4Y9CnuXs7FVIEfWofy9d5cxPF8mRKtNaDQLvxiy5O/xvgMteQ5si7Y1PLdkf8owjvW+hIan7L85gnXJRLd/wQ+ZD+jPjOU1K9ao1JLfeWatgfdtvGcNsBtnzjcu+/+VdWMubNA6i3lNx9MWahl7qvUHa1RqS79pWjord9C/hedFjLsKaByQ72KJLax1437GmqZX+OV2Mz+FP9y0dI52wKuFd3y/CH3Zh1qz1f7x7n62X/USHm3ggqU7pdYyeZ57EedTxI3W1XXw25T1a+DFs4vn/Bz4d7BHm5bOOe4XvzdLeBBRO0YdwzszeYevjFvymYg1xiv1qFvKtx1o17QUi1EXMHYOgfsR8G9Z8hXWiZyrjvFVnFl/kfOxjDdrmeAZeeuapTOce8x6gHquAVEv6jnL51VozH6shdbAn3VnrBvxdtCS7/LdeUv/NUQNxntlG7ypK/dxHWuw/tbzlTVIrMlaKu4Zeu4egm30I+a/WbTL+KaaHoeWh8GX+sZ/I1HnDlixFr9nqY5dtFQztjK+vMurbsH7hqU7J/X+GxyXMGcNWJI9oy9csOJ/PKEr/0cJe96Dvjdl/3g/2ZZ1uH9/Wbe2GnR8AC7Rb8dSPOV2jaIfc7ae6ew7akX/nQRP+kC8j/OO98vgvA95Yj/q2v2SK7Sm5fx5rC2gT/R/YsXacRT7EeMXLMVdcNa4rYrByMd6B676j44+3QFn1U7jTLkzz6nGw/I93m9Yyot5HcdzhPkrELkrYjD8mnGZc2a9pnUO+Q5mPEO/fivG21Grtktty+8eqjM58b873tk+s25c6t0+cNdS/cAYv1Oxfhn6pR20oh+dzvoOZ/b+A6OnAUo=");
            TileMapLayer tileMapLayer = new TileMapLayer("Name", 5, 10, data);

            Assert.That(tileMapLayer, Is.InstanceOf<INamedElement>());
            Assert.That(tileMapLayer.Name, Is.EqualTo("Name"));
            Assert.That(tileMapLayer.Tiles, Is.Not.Null);
            Assert.That(tileMapLayer.Tiles.Count, Is.Not.Zero);
            Assert.That(tileMapLayer.Tiles[tileMapLayer.Tiles.Keys.First()], Is.EqualTo(223));
        }

        // *****************************
        // START Tile Manipulation Tests
        // *****************************
        [Test]
        public void GetTile_GivenAPositionWithNoTile_ReturnsNull()
        {
            Assert.That(tileMapLayer.GetTile(new Position(7, 2)), Is.Null);
        }

        [Test]
        public void GetTile_GivenAPositionWithATile_ReturnsTheTile()
        {
            Position position = new Position(7, 2);
            tileMapLayer.SetTile(position, 2107);

            Assert.That(tileMapLayer.GetTile(position), Is.EqualTo(2107));
        }

        [Test]
        public void MoveTile_GivenAnEmptyPosition_MovesTileToThatPosition()
        {
            Position position1 = new Position(1, 2);
            Position position2 = new Position(3, 4);
            tileMapLayer.SetTile(position1, 20);
            int initialTileCount = tileMapLayer.Tiles.Count;

            tileMapLayer.MoveTile(position1, position2);

            Assert.That(tileMapLayer.GetTile(position1), Is.Null);
            Assert.That(tileMapLayer.GetTile(position2), Is.EqualTo(20));
            // Make sure that the tileCount does not change
            Assert.That(tileMapLayer.Tiles.Count, Is.EqualTo(initialTileCount));
        }

        [Test]
        public void MoveTile_WhenStartingPositionIsEmpty_ThrowsAnException()
        {
            Position startingPosition = new Position(1, 2);
            Position destination = new Position(3, 4);

            Assert.That(() => tileMapLayer.MoveTile(startingPosition, destination), Throws.InvalidOperationException);
        }

        [Test]
        public void MoveTile_WhenDestinationIsOccupied_ThrowsAnException()
        {
            Position startingPosition = new Position(1, 2);
            Position destination = new Position(3, 4);
            tileMapLayer.SetTile(startingPosition, 21);
            tileMapLayer.SetTile(destination, 20);

            Assert.That(() => tileMapLayer.MoveTile(startingPosition, destination), Throws.InvalidOperationException);
        }

        [Test]
        public void SwapTile_GivenTwoPositions_SwapsTilesAtThosePositions()
        {
            Position position1 = new Position(1, 2);
            Position position2 = new Position(3, 4);
            tileMapLayer.SetTile(position1, 20);
            tileMapLayer.SetTile(position2, 21);
            int initialTileCount = tileMapLayer.Tiles.Count;

            tileMapLayer.SwapTile(position1, position2);

            Assert.That(tileMapLayer.GetTile(position1), Is.EqualTo(21));
            Assert.That(tileMapLayer.GetTile(position2), Is.EqualTo(20));
            // Make sure that the number of tiles in the layer does not change
            Assert.That(tileMapLayer.Tiles.Count, Is.EqualTo(initialTileCount));
        }

        [Test]
        public void SwapTile_WhenNoTileExistsAtTheFirstPosition_ThrowsAnException()
        {
            Position position1 = new Position(1, 2);
            Position position2 = new Position(3, 4);
            tileMapLayer.SetTile(position2, 21);

            Assert.That(() => tileMapLayer.SwapTile(position1, position2), Throws.InvalidOperationException);
        }

        [Test]
        public void SwapTile_WhenNoTileExistsInTheSecondPosition_ThrowsAnException()
        {
            Position position1 = new Position(1, 2);
            Position position2 = new Position(3, 4);
            tileMapLayer.SetTile(position1, 21);

            Assert.That(() => tileMapLayer.SwapTile(position1, position2), Throws.InvalidOperationException);
        }

        [Test]
        public void SetTile_GivenAPositionWithNoTile_SetsTileAtThatPosition()
        {
            Position position = new Position(7, 2);
            tileMapLayer.SetTile(position, 200);

            Assert.That(tileMapLayer.Tiles[position], Is.EqualTo(200));
        }

        [Test]
        public void SetTile_GivenAPositionWithATile_SetsANewTileAtThatPosition()
        {
            Position position = new Position(7, 2);
            tileMapLayer.SetTile(position, 200);
            tileMapLayer.SetTile(position, 201);

            Assert.That(tileMapLayer.Tiles[position], Is.EqualTo(201));
        }

        [Test]
        public void RemoveTile_GivenAPositionWithATile_RemovesTile()
        {
            Position position = new Position(7, 2);
            tileMapLayer.SetTile(position, 200);
            int initialTileCount = tileMapLayer.Tiles.Count;

            tileMapLayer.RemoveTile(position);

            Assert.That(tileMapLayer.GetTile(position), Is.Null);
            Assert.That(tileMapLayer.Tiles.Count, Is.EqualTo(initialTileCount - 1));
        }

        [Test]
        public void RemoveTile_GivenAPositionWithNoTile_DoesNotThrowAnException()
        {
            Position position = new Position(7, 2);

            Assert.That(() => tileMapLayer.RemoveTile(position), Throws.Nothing);
        }

        [Test]
        public void Fill_GivenATileId_FillsTheEntireTileMapLayerWithTiles()
        {
            tileMapLayer.Fill(1);

            for (int col = 0; col < tileMapLayer.Width; col++)
            {
                for (int row = 0; row < tileMapLayer.Height; row++)
                {
                    Assert.That(tileMapLayer.Tiles[new Position(col, row)], Is.EqualTo(1));
                }
            }
        }
        // *****************************
        // END Tile Manipulation Tests
        // *****************************

        [Test]
        public void GetObstacles_ReturnsAListOfPositionsOfAllTilesInTheLayerWhichAreViewedAsObstacles()
        {
            var tilePositions = new List<Position>();
            tilePositions.Add(new Position(0, 0));
            tilePositions.Add(new Position(0, 1));
            tilePositions.Add(new Position(1, 3));
            foreach (Position position in tilePositions)
            {
                tileMapLayer.SetTile(position, 1);
            }

            var obstacles = tileMapLayer.GetObstacles();

            Assert.That(obstacles.Count, Is.EqualTo(tilePositions.Count));
            foreach (Position position in tilePositions)
            {
                Assert.That(obstacles.Contains(position), Is.True);
            }
        }
        // TODO: Setting a tile, moving a tile or swapping tiles outside the bounds of the Layer is illegal, write unit tests for these scenarios
    }
}
