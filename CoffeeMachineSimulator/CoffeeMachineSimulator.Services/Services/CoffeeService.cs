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
            if (coffeeToAdd == null)  throw new Exception("You should not add null entries!"); 
            if (coffeeModels.Any(i=> i.Id == coffeeToAdd.Id)) throw new Exception("Already exists an entry with the same Id");
            if (!(coffeeToAdd.Id == Guid.Empty || coffeeToAdd.Name == null || coffeeToAdd.Price <= 0.0f))
                coffeeModels.Add(coffeeToAdd);
           
        }

        public void DeleteCoffee(Guid coffeeId)
        {
            if (coffeeId == Guid.Empty) throw new Exception("The Id given is empty");
            coffeeModels.Remove(coffeeModels.First(i => i.Id == coffeeId));
        }

        public List<CoffeeModel> GetCoffees()
        {
            return coffeeModels;
        }
    }
}
