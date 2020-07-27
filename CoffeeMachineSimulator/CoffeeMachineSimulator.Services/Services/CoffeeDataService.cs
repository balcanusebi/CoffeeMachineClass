using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using CoffeeMachineSimulator.Services.Interfaces;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Services
{
    public class CoffeeDataService : ICoffeeDataService
    {
        private readonly CoffeeContext coffeeContext;
        private readonly IMapper mapper;

        public CoffeeDataService(CoffeeContext context, IMapper mapper)
        {
            coffeeContext = context;
            this.mapper = mapper;
        }
        public async Task AddCoffeeData(CoffeeMachineData coffeeMachineData)
        {
            var myCoffeeDataToAdd = mapper.Map<CoffeeDataEntity>(coffeeMachineData);

            await coffeeContext.CoffeeDataEntities.AddAsync(myCoffeeDataToAdd);
            await coffeeContext.SaveChangesAsync();
        }
    }
}
