using CoffeeMachineSimulator.Services.Models;
using CoffeeMachineSimulator.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineSimulator.Tests.Services
{
    [TestFixture]
    public class CoffeeServiceTests
    {
        private CoffeeService coffeeService;

        [SetUp]
        public void SetUp()
        {
            coffeeService = new CoffeeService();
        }

        [Test]
        public void GetCoffees_Returns_ListOfCoffeeModels()
        {
            var coffeesReturned = coffeeService.GetCoffees();

            Assert.IsNotNull(coffeesReturned);
            Assert.IsTrue(coffeesReturned.Any());
            Assert.IsInstanceOf(typeof(List<CoffeeModel>), coffeesReturned);
            Assert.IsTrue(coffeesReturned.Any(coffeeModel => coffeeModel.Id != Guid.Empty && !string.IsNullOrEmpty(coffeeModel.Name)));
        }

        [Test]
        public void AddNullCoffee_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.AddCoffee(null));

            Assert.AreEqual(ex.Message, "You should not add null entries!");
        }

        //TODO: Add Unit test for Remaining add cases
        [Test]
        public void AddCoffeWithoutPrice_WillNotAddCoffe()
        {
            var initialCoffeeLength = coffeeService.GetCoffees().Count;
            var AddDetailsCoffe = new CoffeeModel();
            AddDetailsCoffe.Id = Guid.NewGuid();
            AddDetailsCoffe.Name = "Your coffe name";

            coffeeService.AddCoffee(AddDetailsCoffe);

            Assert.AreEqual(initialCoffeeLength, coffeeService.GetCoffees().Count);
        }
        [Test]
        public void AddCoffeWithoutName_WillNotAddCoffe()
        {
            var initialCoffeeLength = coffeeService.GetCoffees().Count;
            var AddDetailsCoffe = new CoffeeModel();
            AddDetailsCoffe.Id = Guid.NewGuid();
            AddDetailsCoffe.Price = 22.20f;

            coffeeService.AddCoffee(AddDetailsCoffe);

            Assert.AreEqual(initialCoffeeLength, coffeeService.GetCoffees().Count);
        }
        [Test]
        public void AddCoffeWithoutId_WillNotAddCoffe()
        {
            var initialCoffeeLength = coffeeService.GetCoffees().Count;
            var AddDetailsCoffe = new CoffeeModel();
            AddDetailsCoffe.Name = "Your coffe name";
            AddDetailsCoffe.Price = 22.20f;

            coffeeService.AddCoffee(AddDetailsCoffe);

            Assert.AreEqual(initialCoffeeLength, coffeeService.GetCoffees().Count);
        }
        //Todo: Add Unit test for deleting case
        [Test]
        public void deleteCoffeWithEmptyId()
        {
            var DeleteYourCoffe = new CoffeeModel();

            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(DeleteYourCoffe.Id));

            Assert.AreEqual(ex.Message, "The Id is empty!");
        }
        
    }
}
