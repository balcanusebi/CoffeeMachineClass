using CoffeeMachineSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Interfaces
{
    public interface ICoffeeService
    {
        Task<List<CoffeeModel>> GetCoffees();
        Task AddCoffee(CoffeeModel coffeeToAdd);
        Task DeleteCoffee(Guid coffeeId);
    }
}
