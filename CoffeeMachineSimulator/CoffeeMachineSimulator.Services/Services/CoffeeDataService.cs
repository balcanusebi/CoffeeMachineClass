﻿using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using CoffeeMachineSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var myCoffeeDataToAdd = mapper.Map<CoffeeDataEntity>(coffeeMachineData);

            await coffeeContext.CoffeeDataEntities.AddAsync(myCoffeeDataToAdd);
            await coffeeContext.SaveChangesAsync();
        }

        public async Task<List<EspressoMachineEntity>> GetEspressoCoffees()
        {
            var getEspressoCoffees = await coffeeContext.EspressoMachines.ToListAsync();
            return mapper.Map<List<EspressoMachineEntity>>(getEspressoCoffees);
        }

        public async Task DeleteFirstEspressoCoffee()
        {
            var espressoCoffeeToDelete = await coffeeContext.EspressoMachines.FirstOrDefaultAsync();

            coffeeContext.EspressoMachines.Remove(espressoCoffeeToDelete);

            await coffeeContext.SaveChangesAsync();
        }
    }
}
