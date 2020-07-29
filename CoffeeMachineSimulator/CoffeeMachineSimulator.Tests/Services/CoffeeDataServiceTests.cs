using CoffeeMachineSimulator.Services.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Tests.Services
{
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
        public void MyNewTest()
        {
            var test = 1;

            Assert.AreEqual(1, test);
        }

        [Test]
        public async Task MultiplyCoffee_MultipliesCoffee()
        {
            int times = 5;
            int initialCoffeesCount = await Context.Coffees.CountAsync(x=>x.Id != null);
            Guid coffeeIdToMultiply = (await Context.Coffees.FirstAsync()).Id;

            await coffeeDataService.MultiplyCoffee(coffeeIdToMultiply, times);
            int actualCoffeesCount = await Context.Coffees.CountAsync(x => x.Id != null);


            Assert.AreEqual(initialCoffeesCount + 5, actualCoffeesCount);
        }

    }
}
