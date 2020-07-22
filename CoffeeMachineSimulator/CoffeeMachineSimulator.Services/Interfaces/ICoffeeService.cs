using CoffeeMachineSimulator.Services.Models;
using System;
using System.Collections.Generic;

namespace CoffeeMachineSimulator.Services.Interfaces
{
    public interface ICoffeeService
    {
        List<CoffeeModel> GetCoffees();
        void AddCoffee(CoffeeModel coffeeToAdd);
        void DeleteCoffee(Guid coffeeId);
        void DrinkCoffee(Guid coffeeId); //TODO: add new property in object that marks if the cofee is full or not
        void UpdateCoffee(CoffeeModel coffeeToUpdate);
        void GetAllCoffeesFromEspressoMachine(Guid espressoMachineId);
    }
}
