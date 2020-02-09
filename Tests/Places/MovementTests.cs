using NUnit.Framework;
using System;
using System.IO;
using Turnable.Characters;
using Turnable.Places;
using Turnable.Utilities;

namespace Tests.Places
{
    [TestFixture]
    public class MovementTests
    {
        [Test]
        public void ParameterlessConstructorExists()
        {
            var movement = new Movement();

            Assert.That(movement.Status, Is.EqualTo(MovementStatus.None));
            Assert.That(movement.Path, Is.Not.Null);
        }
    }
}