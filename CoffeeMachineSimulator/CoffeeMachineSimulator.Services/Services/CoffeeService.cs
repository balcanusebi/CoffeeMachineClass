using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Services.Interfaces;
using CoffeeMachineSimulator.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly IMapper mapper;
        private readonly IEspressoMachineService espressoMachineService;
        private readonly CoffeeContext context;

        public CoffeeService(IMapper mapper, IEspressoMachineService espressoMachineService, CoffeeContext context)
        {
            this.mapper = mapper;
            this.espressoMachineService = espressoMachineService;
            this.context = context;
        }

        public async Task AddCoffee(CoffeeModel coffeeToAdd)
        {
            if (coffeeToAdd == null) throw new Exception("You should not add null entries!");
            if (!IsCoffeeValid(coffeeToAdd)) throw new Exception("The coffee you are trying to add is not valid");

            var coffeeEntityToAdd = mapper.Map<CoffeeEntity>(coffeeToAdd);
            coffeeEntityToAdd.EspressoMachine = await espressoMachineService.GetEspressoMachine(coffeeToAdd.IsEsspreso);

            await context.Coffees.AddAsync(coffeeEntityToAdd);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCoffee(Guid coffeeId)
        {
            if(coffeeId == Guid.Empty || coffeeId == null) throw new Exception("Please provide an ID!");

            var coffeeFromDb = await context.Coffees.FirstOrDefaultAsync(x => x.Id == coffeeId);
            if (coffeeFromDb == null) throw new Exception("The coffee you are trying to delete does not exist!");

            context.Coffees.Remove(coffeeFromDb);

            await context.SaveChangesAsync();
        }

        public async Task<List<CoffeeModel>> GetCoffees()
        {
            var coffeeEntities = await context.Coffees.ToListAsync();

            return mapper.Map<List<CoffeeModel>>(coffeeEntities);
        }

        private bool IsCoffeeValid(CoffeeModel model)
        {
            return !string.IsNullOrEmpty(model.Name) && model.Price != 0.0f;
        }
    }
}
