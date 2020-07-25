using CoffeeMachineSimulator.Services.Interfaces;
using CoffeeMachineSimulator.Services.Models;
using CoffeeMachineSimulator.Services.Services;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Tests.Services
{
    [TestFixture]
    public class CoffeeServiceTests : ServiceSetUp
    {
        private Mock<IEspressoMachineService> espressoMachineMock;
        private CoffeeService coffeeService;

        public CoffeeServiceTests() : base() {}

        [SetUp]
        public void SetUp()
        {
            espressoMachineMock = new Mock<IEspressoMachineService>();
            coffeeService = new CoffeeService(Mapper, espressoMachineMock.Object, Context);
        }

        [Test]
        public async Task GetCoffees_Returns_ListOfCoffeeModels()
        {
            var coffeesReturned = await coffeeService.GetCoffees();

            Assert.IsNotNull(coffeesReturned);
            Assert.IsTrue(coffeesReturned.Any());
            Assert.IsInstanceOf(typeof(List<CoffeeModel>), coffeesReturned);
            Assert.IsTrue(coffeesReturned.Any(coffeeModel => coffeeModel.Id != Guid.Empty && !string.IsNullOrEmpty(coffeeModel.Name)));
        }

        [Test]
        public void AddNullCoffee_Throws_NewException()
        {
            var ex = Assert.ThrowsAsync<Exception>(() => coffeeService.AddCoffee(null));
            
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

            var ex = Assert.ThrowsAsync<Exception>(() => coffeeService.AddCoffee(coffeeToAdd));

            Assert.AreEqual(ex.Message, "The coffee you are trying to add is not valid");
        }

        [Test]
        public void DeleteCoffeeWithEmptyId_Throws_NewException()
        {
            var ex = Assert.ThrowsAsync<Exception>(() => coffeeService.DeleteCoffee(Guid.Empty));

            Assert.AreEqual(ex.Message, "Please provide an ID!");
        }

        [Test]
        public void DeleteCoffeeWithNonExistingId_Throws_NewException()
        {
            var ex = Assert.ThrowsAsync<Exception>(() => coffeeService.DeleteCoffee(Guid.NewGuid()));

            Assert.AreEqual(ex.Message, "The coffee you are trying to delete does not exist!");
        }

        [Test]
        public async Task DeleteCoffee_DeletesCoffee()
        {
            var expectedCountOfCoffees = (await coffeeService.GetCoffees()).Count;
            var coffeeToRemove = (await coffeeService.GetCoffees()).First();

            await coffeeService.DeleteCoffee(coffeeToRemove.Id);

            var actualCountOfCoffees = (await coffeeService.GetCoffees()).Count;

            Assert.AreNotEqual(expectedCountOfCoffees, actualCountOfCoffees);
        }
    }
}
