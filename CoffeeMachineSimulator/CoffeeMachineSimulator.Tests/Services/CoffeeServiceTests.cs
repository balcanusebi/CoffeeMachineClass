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

        [Test]
        public void DrinkCoffeeWithNullOrEmptyId_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.DrinkCoffee(Guid.Empty));

            Assert.AreEqual(ex.Message, "Please provide an ID!");
        }

        [Test]
        public void DrinkCoffeeWhichIsEmpty_Throws_Exception()
        {
            var emptyCoffee = Builder<CoffeeModel>.CreateNew().With(x => x.Filled = false).Build();

            coffeeService.AddCoffee(emptyCoffee);
            var ex = Assert.Throws<Exception>(() => coffeeService.DrinkCoffee(coffeeService.GetCoffees().LastOrDefault().Id));

            Assert.AreEqual(ex.Message, "The coffee you are trying to drink has already been drank!");
        }

        [Test]
        public void DrinkCoffee_DrinksCoffee()
        {
            var coffeeToDrink = coffeeService.GetCoffees()[8];

            coffeeService.DrinkCoffee(coffeeToDrink.Id);

            Assert.AreEqual(coffeeToDrink.Filled, false);
        }

        [Test]
        public void UpdateCoffeeNullCoffee_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.UpdateCoffee(null));

            Assert.AreEqual(ex.Message, "The coffee you provided is null");
        }

        [Test]
        public void UpdateInvalidCoffee_Throws_NewException()
        {
            var invalidCoffee = Builder<CoffeeModel>.CreateNew()
                    .With(x => x.Id = Guid.Empty)
                    .With(x => x.Name = "")
                    .With(x => x.Price = 0.0f)
                .Build();
            var ex = Assert.Throws<Exception>(() => coffeeService.UpdateCoffee(invalidCoffee));

            Assert.AreEqual(ex.Message, "The coffee you provided is not valid");
        }

        [Test]
        public void UpdateCoffeeNoExistingId_Throws_NewException()
        {
            var wrongIdCoffee = Builder<CoffeeModel>.CreateNew().With(x => x.Id = Guid.NewGuid()).Build();
            var ex = Assert.Throws<Exception>(() => coffeeService.UpdateCoffee(wrongIdCoffee));

            Assert.AreEqual(ex.Message, "There is no coffee with the ID you provided!");
        }

        [Test]
        public void UpdateCoffee_UpdatesCoffee()
        {
            var coffeeToUpdate = Builder<CoffeeModel>.CreateNew()
                    .With(x => x.Name = "Tea")
                    .With(x => x.Id = coffeeService.GetCoffees()[0].Id)
                .Build();

            coffeeService.UpdateCoffee(coffeeToUpdate);

            Assert.IsTrue(coffeeService.GetCoffees()[0].Name == "Tea");
        }

        [Test]
        public void GetAllCoffeesFromNullEspressoMachineId_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.GetAllCoffeesFromEspressoMachine(Guid.Empty));

            Assert.AreEqual(ex.Message, "Please provide an ID!");
        }

        [Test]
        public void GetAllCoffeesFromWrongEspressoMachineId_Throws_NewException()
        {
            var ex = Assert.Throws<Exception>(() => coffeeService.GetAllCoffeesFromEspressoMachine(Guid.NewGuid()));

            Assert.AreEqual(ex.Message, "There is no espresso machine with that ID");
        }

        [Test]
        public void GetAllCoffeesFromEspressoMachine_Returns_The_Right_Coffees()
        {
            var espressoMachineId = coffeeService.GetCoffees()[0].EspressoMachine.Id;

            Assert.IsTrue(coffeeService.GetAllCoffeesFromEspressoMachine(espressoMachineId).All(x => x.EspressoMachine.Id==espressoMachineId));
        }
    }
}
