using System;
using NUnit.Framework;
using Turnable.Utilities;

namespace Tests.Utilities
{
    [TestFixture]
    public class NamedElementCollectionTests
    {
        [Test]
        public void Add_AddsElementThatCanBeReferencedByIndexOrName()
        {
            //_elementList.Add(layer);

            //Assert.That(_elementList[0], Is.SameAs(layer));
            //Assert.That(_elementList[layer.Name], Is.SameAs(layer));
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