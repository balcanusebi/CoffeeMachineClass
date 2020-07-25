using CoffeeMachineSimulator.Services.Models;
using NUnit.Framework;

namespace CoffeeMachineSimulator.Tests
{
    [TestFixture]
    public class ValueReferenceTypesTests
    {
        [Test]
        public void TestValueTypes()
        {
            int nbToTest = 245;
            int nbToAssign = nbToTest;

            nbToTest = 20;

            Assert.AreNotEqual(nbToAssign, nbToTest);
        }

        [Test]
        public void ReferenceTypes()
        {
            var coffeModel = new CoffeeModel
            {
                Name = "Test123"
            };

            var secondeCoffeeModel = coffeModel;
            coffeModel.Name = "My changed name";

            Assert.AreEqual(coffeModel.Name, secondeCoffeeModel.Name);
        }
    }
}
