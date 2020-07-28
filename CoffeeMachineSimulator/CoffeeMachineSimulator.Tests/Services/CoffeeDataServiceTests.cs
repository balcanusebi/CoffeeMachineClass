using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using CoffeeMachineSimulator.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Tests.Services
{
    [TestFixture]
    public class CoffeeDataServiceTests : ServiceSetUp
    {
        private CoffeeDataService coffeeDataService;
        public CoffeeDataServiceTests() : base() { }

        [SetUp]
        public void SetUp()
        {
            coffeeDataService = new CoffeeDataService(Context, Mapper);
        }
        [Test]
        public async Task DeleteFirstCoffeeData_DeletesCoffeeData()
        {
            var expectedCountOfCoffeeDatas = (await coffeeDataService.GetCoffeeDatas()).Count;

            await coffeeDataService.DeleteFirstCoffeeData();

            var actualCountOfCoffeeDatas = (await coffeeDataService.GetCoffeeDatas()).Count;

            Assert.AreNotEqual(expectedCountOfCoffeeDatas, actualCountOfCoffeeDatas);
        }

        [Test]
        public void AddNullCoffeeData_Throws_NewException()
        {
            var ex = Assert.ThrowsAsync<Exception>(() => coffeeDataService.AddCoffeeData(null));

            Assert.AreEqual(ex.Message, "You should not add null entry");
        }

        [Test]
        public async Task GetCoffeeDatas_Returns_ListOfCoffeeDataEntities()
        {
            var coffeeDatasReturned = await coffeeDataService.GetCoffeeDatas();

            Assert.IsNotNull(coffeeDatasReturned);
            Assert.IsInstanceOf(typeof(List<CoffeeMachineData>), coffeeDatasReturned);
        }
    }

}
