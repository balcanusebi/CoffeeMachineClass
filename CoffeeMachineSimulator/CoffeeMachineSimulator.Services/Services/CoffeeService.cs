using CoffeeMachineSimulator.Services.Interfaces;
using CoffeeMachineSimulator.Services.Models;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineSimulator.Services.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly List<CoffeeModel> coffeeModels;

        public CoffeeService()
        {
            coffeeModels = Builder<CoffeeModel>.CreateListOfSize(10).Build().ToList();
        }

        public void AddCoffee(CoffeeModel coffeeToAdd)
        {
            if (coffeeToAdd == null) throw new Exception("You should not add null entries!");

            coffeeModels.Add(coffeeToAdd);
        }

        public void DeleteCoffee(Guid coffeeId)
        {
            throw new NotImplementedException();
        }

        public List<CoffeeModel> GetCoffees()
        {
            return coffeeModels;
        }
    }
}
