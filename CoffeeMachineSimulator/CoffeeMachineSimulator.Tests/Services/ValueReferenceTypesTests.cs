using CoffeeMachineSimulator.Services.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineSimulator.Tests.Services
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

            Assert.AreEqual(nbToAssign, nbToTest);
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
