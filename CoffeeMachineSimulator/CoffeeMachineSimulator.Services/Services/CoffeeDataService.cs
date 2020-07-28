using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using CoffeeMachineSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            if (coffeeMachineData == null) throw new Exception("You should not add null entry");

            var myCoffeeDataToAdd = mapper.Map<CoffeeDataEntity>(coffeeMachineData);

            await coffeeContext.CoffeeDataEntities.AddAsync(myCoffeeDataToAdd);
            await coffeeContext.SaveChangesAsync();
        }

        public async Task DeleteFirstCoffeeData()
        {
            var coffeeToDelete = await coffeeContext.CoffeeDataEntities.FirstOrDefaultAsync();

            coffeeContext.CoffeeDataEntities.Remove(coffeeToDelete);

            await coffeeContext.SaveChangesAsync();
        }

        public async Task<List<CoffeeMachineData>> GetCoffeeDatas()
        {
            var coffeeDataEntities = await coffeeContext.CoffeeDataEntities.ToListAsync();

            return mapper.Map<List<CoffeeMachineData>>(coffeeDataEntities);
        }
    }
}
