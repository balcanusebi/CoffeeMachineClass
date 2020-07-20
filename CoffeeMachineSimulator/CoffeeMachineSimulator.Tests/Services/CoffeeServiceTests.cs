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
        public void AddCoffeWithoutPrice_DoesNotAddCoffee()
        {
            var LengthList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();
            AddCoffeeDetails.Id = Guid.NewGuid();
            AddCoffeeDetails.Name = "Your coffe name";
            coffeeService.AddCoffee(AddCoffeeDetails);
            Assert.AreEqual(LengthList, coffeeService.GetCoffees().Count);

        }
        [Test]
        public void AddCoffeWithoutName_DoesNotAddCoffee()
        {
            var LengthList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();
            AddCoffeeDetails.Id = Guid.NewGuid();
            AddCoffeeDetails.Price = 10.68f;
            coffeeService.AddCoffee(AddCoffeeDetails);
            Assert.AreEqual(LengthList, coffeeService.GetCoffees().Count);

        }
        [Test]
        public void AddCoffeWithoutId_DoesNotAddCoffee()
        {
            var LengthList = coffeeService.GetCoffees().Count;
            var AddCoffeeDetails = new CoffeeModel();
            AddCoffeeDetails.Name = "Your coffe name";
            AddCoffeeDetails.Price = 22.20f;
            coffeeService.AddCoffee(AddCoffeeDetails);
            Assert.AreEqual(LengthList, coffeeService.GetCoffees().Count);

        }

        [Test]
        //Todo: Add Unit test for deleting case
        public void DeletedCoffee()
        {
            var deletedcoffee = coffeeService.GetCoffees().First();

            coffeeService.DeleteCoffee(deletedcoffee.Id);
            Assert.IsTrue(deletedcoffee != coffeeService.GetCoffees().First());
        }
    }
}
