using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Services
{
    public class EspressoMachineService : IEspressoMachineService
    {
        private readonly CoffeeContext coffeeContext;

        public EspressoMachineService(CoffeeContext coffeeContext)
        {
            this.coffeeContext = coffeeContext;
        }

        public void AddEspressoMachine(EspressoMachineEntity espressoMachine)
        {
            if (string.IsNullOrEmpty(espressoMachine.Name)) throw new Exception("Name cannot be empty");

            coffeeContext.EspressoMachines.Add(espressoMachine);
            coffeeContext.SaveChanges();
        }

        public async Task<EspressoMachineEntity> GetEspressoMachine(bool isEspressoMachine)
        {
            var espressoMachineToReturn = await coffeeContext.EspressoMachines.FirstOrDefaultAsync(x => x.IsEspressor == isEspressoMachine);

            if (espressoMachineToReturn != null) return espressoMachineToReturn;

            var espressorMachineToAdd = GetNewEspressoMachine(isEspressoMachine);
            await coffeeContext.EspressoMachines.AddAsync(espressorMachineToAdd);
            await coffeeContext.SaveChangesAsync();

            return espressorMachineToAdd;
        }

        private EspressoMachineEntity GetNewEspressoMachine(bool isEspressoMachine)
        {
            var espressoMachineToReturn = new EspressoMachineEntity { IsEspressor = isEspressoMachine };

            if (isEspressoMachine == true)
                espressoMachineToReturn.Name = "Espressor";
            else
                espressoMachineToReturn.Name = "Coffee Machine";

            return espressoMachineToReturn;
        }
    }
}
