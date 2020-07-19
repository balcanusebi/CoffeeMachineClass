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
        public void AddAlreadyExistingCoffee_Throws_NewException()
        {            
            var coffeeAdded = new CoffeeModel(); 
            coffeeAdded = coffeeService.GetCoffees()[0];
            var ex= Assert.Throws<Exception>(() => coffeeService.AddCoffee(coffeeAdded));

            Assert.AreEqual(ex.Message, "Already exists an entry with the same Id");
        }

        [Test]
        public void AddCoffeeWithEmptyPrice_DoesNotAddCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Id = Guid.NewGuid();
            coffeeAdded.Name = "Nume";

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyName_DoesNotAddCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Id = Guid.NewGuid();
            coffeeAdded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyId_DoesNotAddCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Name = "Nume";
            coffeeAdded.Price = long.MaxValue;
            
            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyNameAndEmptyPrice_DoesNotAddCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Id = Guid.NewGuid();

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyIdAndEmptyPrice_DoesNotAddCoffee()
        {
            var intialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Name = "Nume";

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(intialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyIdAndEmptyName_DoesNotAddCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithExistingName_DoesNotAddNewCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Name = coffeeService.GetCoffees()[0].Name;
            coffeeAdded.Id = Guid.NewGuid();
            coffeeAdded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithExistingId_DoesNotAddNewCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Id = coffeeService.GetCoffees()[0].Id;
            coffeeAdded.Name = "Nume";
            coffeeAdded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithExistingPrice_DoesNotAddNewCoffee()
        {
            var initialSize = coffeeService.GetCoffees().Count;
            var coffeeAdded = new CoffeeModel();
            coffeeAdded.Price = coffeeService.GetCoffees()[0].Price;
            coffeeAdded.Name = "Nume";
            coffeeAdded.Id = Guid.NewGuid();

            coffeeService.AddCoffee(coffeeAdded);
            Assert.AreEqual(initialSize, coffeeService.GetCoffees().Count);
        }

        [Test]
        //Todo: Add Unit test for deleting case
        public void DeleteCoffeeWithEmptyId_Throws_NewException()
        {
            var coffeeToDelete = new CoffeeModel();
            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(coffeeToDelete.Id));

            Assert.AreEqual(ex.Message, "The Id given is empty");
        }

        [Test]
        public void DeleteCoffee_Deletes_Coffee()
        {
            var coffeeToDelete = coffeeService.GetCoffees().First();

            coffeeService.DeleteCoffee(coffeeToDelete.Id);
            Assert.IsTrue(coffeeToDelete != coffeeService.GetCoffees().First());
        }
    }
}
