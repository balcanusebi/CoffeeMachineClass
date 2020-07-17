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

        [Test]

        //TODO: Add Unit test for Remaining add cases
        public void AddExistingCoffee_Throws_NewException()
        {            
            var coffeeadded = new CoffeeModel(); coffeeadded = coffeeService.GetCoffees()[0];
            var ex= Assert.Throws<Exception>(() => coffeeService.AddCoffee(coffeeadded));

            Assert.AreEqual(ex.Message, "Already exists an entry with the same Id");
        }

        [Test]
        public void AddEmptyFieldCoffee_Throws_NewException()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel(); coffeeadded.Id = Guid.NewGuid(); coffeeadded.Name = "Nume";
            coffeeService.AddCoffee(coffeeadded);

            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);

            var coffeeadded1 = new CoffeeModel(); coffeeadded1.Id = Guid.NewGuid(); coffeeadded1.Price = 8.88f;
            coffeeService.AddCoffee(coffeeadded1);

            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);

            var coffeeadded2 = new CoffeeModel(); coffeeadded2.Name = "Nume"; coffeeadded2.Price = 8.88f;
            coffeeService.AddCoffee(coffeeadded2);

            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);

            var coffeeadded3 = new CoffeeModel(); coffeeadded3.Id = Guid.NewGuid();
            coffeeService.AddCoffee(coffeeadded3);

            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);

            var coffeeadded4 = new CoffeeModel(); coffeeadded4.Name = "Nume";
            coffeeService.AddCoffee(coffeeadded4);

            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);

            var coffeeadded5 = new CoffeeModel(); coffeeadded5.Price = 8.88f;
            coffeeService.AddCoffee(coffeeadded5);

            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        //Todo: Add Unit test for deleting case
        public void DeleteCoffeeWithEmptyId_Throws_NewException()
        {
            var coffeeadded = new CoffeeModel();
            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(coffeeadded.Id));

            Assert.AreEqual(ex.Message, "The Id given is empty");
        }

    }
}
