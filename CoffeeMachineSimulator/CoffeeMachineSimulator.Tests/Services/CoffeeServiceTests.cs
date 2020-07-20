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
        public void AddCoffeWithoutPrice_DoesNotAddCoffee()
        {
            var CoffeeLenghtList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();
            AddCoffeeDetails.Id = Guid.NewGuid();
            AddCoffeeDetails.Name = "Your coffe name";
            coffeeService.AddCoffee(AddCoffeeDetails);
            Assert.AreEqual(CoffeeLenghtList, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeWithoutName_DoesNotAddCoffee()
        {
            var CoffeeLenghtList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();
            AddCoffeeDetails.Id = Guid.NewGuid();
            AddCoffeeDetails.Price = 10.68f;
            coffeeService.AddCoffee(AddCoffeeDetails);
            Assert.AreEqual(CoffeeLenghtList, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffeWithoutId_DoesNotAddCoffee()
        {
            var CoffeeLenghtList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();
            AddCoffeeDetails.Name = "Your coffe name";
            AddCoffeeDetails.Price = 22.20f;
            coffeeService.AddCoffee(AddCoffeeDetails);
            Assert.AreEqual(CoffeeLenghtList, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffee_ExistingCoffee_ThrowsException()
        {
            
            var AddCoffeeDetails = coffeeService.GetCoffees()[0];

            var ex = Assert.Throws<Exception>(() => coffeeService.AddCoffee(AddCoffeeDetails));
            Assert.AreEqual(ex.Message, "Already exists an entry with the same Id");


        }

        [Test]
        public void AddCoffee_ExistingCoffeePrice_DoesNotAddCoffee()
        {
            var CoffeeLenghtList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();

            AddCoffeeDetails.Name = "hipoooo";
            AddCoffeeDetails.Id = Guid.NewGuid();
            AddCoffeeDetails.Price = coffeeService.GetCoffees()[0].Price;

            Assert.AreEqual(CoffeeLenghtList, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffee_ExistingCoffeeName_DoesNotAddCoffee()
        {
            var CoffeeLenghtList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();

            AddCoffeeDetails.Name = coffeeService.GetCoffees()[0].Name;
            AddCoffeeDetails.Id = Guid.NewGuid();
            AddCoffeeDetails.Price = 15.58f;

            Assert.AreEqual(CoffeeLenghtList, coffeeService.GetCoffees().Count);
        }

        [Test]
        public void AddCoffee_ExistingCoffeeId_DoesNotAddCoffee()
        {
            var CoffeeLenghtList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();

            AddCoffeeDetails.Name = "malamala";
            AddCoffeeDetails.Id = coffeeService.GetCoffees()[0].Id;
            AddCoffeeDetails.Price = 15.58f;

            Assert.AreEqual(CoffeeLenghtList, coffeeService.GetCoffees().Count);
        }

            //Todo: Add Unit test for deleting case
            [Test]
        public void DeleteCoffeeWithEmptyId_Throws_NewException()
        {
            var coffeeToDelete = new CoffeeModel();
            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(coffeeToDelete.Id));

            Assert.AreEqual(ex.Message, "The Id given is empty");
        }
        [Test]
        public void DeletedCoffee()
        {
            var deletedcoffee = coffeeService.GetCoffees().First();

            coffeeService.DeleteCoffee(deletedcoffee.Id);

            Assert.IsTrue(deletedcoffee != coffeeService.GetCoffees().First());
        }
    }
}
