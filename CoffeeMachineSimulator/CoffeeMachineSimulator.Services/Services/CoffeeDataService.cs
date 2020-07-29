using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using CoffeeMachineSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task MultiplyCoffee(Guid coffeeId, int times)
        {
            var coffeeToMultiply = await coffeeContext.Coffees.FirstOrDefaultAsync(x => x.Id == coffeeId);

            if (coffeeToMultiply == null) coffeeToMultiply = await coffeeContext.Coffees.FirstOrDefaultAsync();

            for(int i=0; i<times; i++)
            {
                await coffeeContext.Coffees.AddAsync(GetDuplicatedCoffee(coffeeToMultiply));
            }

            await coffeeContext.SaveChangesAsync();
        }

        private CoffeeEntity GetDuplicatedCoffee(CoffeeEntity coffeeToDuplicate)
        {
            return new CoffeeEntity
            {
                EspressoMachineId = coffeeToDuplicate.EspressoMachineId,
                Name = coffeeToDuplicate.Name,
                Price = coffeeToDuplicate.Price,
                Sweetness = coffeeToDuplicate.Sweetness
            };
        }
    }
}
