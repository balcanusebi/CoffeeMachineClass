using CoffeeMachineSimulator.Services.Services;
using NUnit.Framework;
using CoffeeMachineSimulator.Services.Enums;
using Moq;
using CoffeeMachineSimulator.Services.Interfaces;
using System.Collections.Generic;
using CoffeeMachineSimulator.Services.Models;
using FizzWare.NBuilder;
using System.Linq;

namespace CoffeeMachineSimulator.Tests.Services
{
    [TestFixture]
    public class EspressoMachineServiceTests
    {
        private Mock<ICoffeeService> mockedCoffeeService;

        [SetUp]
        public void SetUp()
        {
            mockedCoffeeService = new Mock<ICoffeeService>(); 
        }

        [Test]
        public void GiveMeACoffee_ReturnsACoffee()
        {
            mockedCoffeeService.Setup(x => x.GetCoffees()).Returns(GetMockedCoffeeModels());
            var service = new EspressoMachineService(mockedCoffeeService.Object);
            
            var returnedCoffee = service.GiveMeACoffee(SweetnessEnum.Sweet);

            Assert.AreEqual(SweetnessEnum.Sweet, returnedCoffee.Sweetness);
        }

        private List<CoffeeModel> GetMockedCoffeeModels()
        {
            return Builder<CoffeeModel>.CreateListOfSize(10)
                    .TheFirst(5)
                        .With(x => x.Sweetness = SweetnessEnum.Sweet)
                    .TheLast(5)
                        .With(x => x.Sweetness = SweetnessEnum.LessSweet)
                    .Build()
                    .ToList();
        }
    }
}
