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
    }
}
