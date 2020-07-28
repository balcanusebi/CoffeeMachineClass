using CoffeeMachineSimulator.Data.Entities;
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
            var expectedCountOfCoffees = (await coffeeDataService.GetCoffeeDatas()).Count;

            await coffeeDataService.DeleteFirstCoffeeData();

            var actualCountOfCoffees = (await coffeeDataService.GetCoffeeDatas()).Count;

            Assert.AreNotEqual(expectedCountOfCoffees, actualCountOfCoffees);
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
            var coffeesReturned = await coffeeDataService.GetCoffeeDatas();

            Assert.IsNotNull(coffeesReturned);
            Assert.IsInstanceOf(typeof(List<CoffeeDataEntity>), coffeesReturned);
        }
    }

}
