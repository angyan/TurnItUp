using System;
using NUnit.Framework;
using Turnable.Utilities;

namespace Tests.Utilities
{
    [TestFixture]
    public class NamedElementCollectionTests
    {
        [Test]
        public void Add_GivenANamedElement_AddsTheNamedElementThatCanThenBeReferencedByIndexOrName()
        {
            var tileMapLayer = new TileMapLayer("Name", 5, 10);
            var namedElementCollection = new NamedElementCollection<TileMapLayer>();

            namedElementCollection.Add(tileMapLayer);

            Assert.That(namedElementCollection[0], Is.SameAs(tileMapLayer));
            Assert.That(namedElementCollection[tileMapLayer.Name], Is.SameAs(tileMapLayer));
        }

        [Test]
        public void Add_CalledMultipleTimesWithDifferentNamedElements_KeepsTheOrderInWhichTheNamedElementsAreAdded()
        {
            var tileMapLayers = new TileMapLayer[3];
            var namedElementCollection = new NamedElementCollection<TileMapLayer>();

            for (int index = 0; index < 3; index++)
            {
                tileMapLayers[index] = new TileMapLayer(String.Format("Layer {0}", index), 5, 10);

                namedElementCollection.Add(tileMapLayers[index]);
            }

            for (int index = 0; index < 3; index++)
            {
                Assert.That(namedElementCollection[index], Is.SameAs(tileMapLayers[index]));
            }
        }

        [Test]
        public void Add_WhenAddingANamedElementWhenANamedElementWithTheSameNameAlreadyExists_ThrowsAnException()
        {
            var tileMapLayer = new TileMapLayer("Name", 5, 10);
            var namedElementCollection = new NamedElementCollection<TileMapLayer>();

            namedElementCollection.Add(tileMapLayer);
            Assert.That(() => namedElementCollection.Add(tileMapLayer), Throws.ArgumentException);
        }
    }
}