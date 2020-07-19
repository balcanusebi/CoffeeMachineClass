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
            var coffeeadded = new CoffeeModel(); 
            coffeeadded = coffeeService.GetCoffees()[0];
            var ex= Assert.Throws<Exception>(() => coffeeService.AddCoffee(coffeeadded));

            Assert.AreEqual(ex.Message, "Already exists an entry with the same Id");
        }

        [Test]
        public void AddCoffeeWithEmptyPrice_DoesNotAddCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Id = Guid.NewGuid();
            coffeeadded.Name = "Nume";

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyName_DoesNotAddCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Id = Guid.NewGuid();
            coffeeadded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyId_DoesNotAddCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Name = "Nume";
            coffeeadded.Price = long.MaxValue;
            
            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyNameAndEmptyPrice_DoesNotAddCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Id = Guid.NewGuid();

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyIdAndEmptyPrice_DoesNotAddCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Name = "Nume";

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithEmptyIdAndEmptyName_DoesNotAddCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithExistingName_DoesNotAddNewCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Name = coffeeService.GetCoffees()[0].Name;
            coffeeadded.Id = Guid.NewGuid();
            coffeeadded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithExistingId_DoesNotAddNewCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Id = coffeeService.GetCoffees()[0].Id;
            coffeeadded.Name = "Nume";
            coffeeadded.Price = long.MaxValue;

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeeWithExistingPrice_DoesNotAddNewCoffee()
        {
            var marime = coffeeService.GetCoffees().Count;
            var coffeeadded = new CoffeeModel();
            coffeeadded.Price = coffeeService.GetCoffees()[0].Price;
            coffeeadded.Name = "Nume";
            coffeeadded.Id = Guid.NewGuid();

            coffeeService.AddCoffee(coffeeadded);
            Assert.AreEqual(marime, coffeeService.GetCoffees().Count);
        }

        [Test]
        //Todo: Add Unit test for deleting case
        public void DeleteCoffeeWithEmptyId_Throws_NewException()
        {
            var coffeetodelete = new CoffeeModel();
            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(coffeetodelete.Id));

            Assert.AreEqual(ex.Message, "The Id given is empty");
        }

        [Test]
        public void DeleteCoffee_Deletes_Coffee()
        {
            var coffeetodelete = coffeeService.GetCoffees().First();

            coffeeService.DeleteCoffee(coffeetodelete.Id);
            Assert.IsTrue(coffeetodelete != coffeeService.GetCoffees().First());
        }
    }
}
