using System;
using NUnit.Framework;
using Turnable.Utilities;

namespace Tests.Utilities
{
    [TestFixture]
    public class NamedElementCollectionTests
    {
        [Test]
        public void Add_GivenAnObjectThatImplementsINamedElement_AddsObjectThatCanThenBeReferencedByIndexOrName()
        {
            TileMapLayer tileMapLayer = new TileMapLayer("Name");
            var namedElementCollection = new NamedElementCollection<TileMapLayer>();

            namedElementCollection.Add(tileMapLayer);

            Assert.That(namedElementCollection[0], Is.SameAs(tileMapLayer));
            Assert.That(namedElementCollection[tileMapLayer.Name], Is.SameAs(tileMapLayer));
        }

        [Test]
        public void Add_WhenMultipleElementsAreAdded_KeepsTheOrderInWhichTheElementsAreAdded()
        {
            //Layer[] layers = new Layer[3];

            //for (int i = 0; i < 3; i++)
            //{
            //    layers[i] = TiledFactory.BuildLayer();
            //    layers[i].Name = String.Format("Layer {0}", i);
            //    _elementList.Add(layers[i]);
            //}

            //for (int i = 0; i < 3; i++)
            //{
            //    Assert.That(_elementList[i], Is.SameAs(layers[i]));
            //}
        }

        [Test]
        public void Add_WhenAnElementWithTheSameNameAlreadyExists_ThrowsAnException()
        {
            //Layer layer = TiledFactory.BuildLayer();

            //_elementList.Add(layer);
            //Assert.That(() => _elementList.Add(layer), Throws.ArgumentException);
        }
    }
}