using CoffeeMachineSimulator.Services.Models;
using CoffeeMachineSimulator.Services.Services;
using FizzWare.NBuilder;
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
        public void AddInvalidCoffee_Throws_NewException()
        {
            var coffeeToAdd = Builder<CoffeeModel>.CreateNew()
                                .With(x => x.Id = Guid.Empty)
                                .With(x => x.Name = "")
                                .With(x=>x.Price = 0.0f)
                                .Build();

            var ex = Assert.Throws<Exception>(() => coffeeService.AddCoffee(coffeeToAdd));

            Assert.AreEqual(ex.Message, "The coffee you are trying to add is not valid");
        }

        [Test]
        public void DeleteCoffeeWithEmptyId_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(Guid.Empty));

            Assert.AreEqual(ex.Message, "Please provide an ID!");
        }

        [Test]
        public void DeleteCoffeeWithNonExistingId_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.DeleteCoffee(Guid.NewGuid()));

            Assert.AreEqual(ex.Message, "The coffee you are trying to delete does not exist!");
        }

        [Test]
        public void DeleteCoffee_DeletesCoffee()
        {
            var initialCountOfCoffees = coffeeService.GetCoffees().Count;
            var coffeeToRemove = coffeeService.GetCoffees().First();

            coffeeService.DeleteCoffee(coffeeToRemove.Id);

            Assert.AreNotEqual(initialCountOfCoffees, coffeeService.GetCoffees().Count);
        }
    }
}
