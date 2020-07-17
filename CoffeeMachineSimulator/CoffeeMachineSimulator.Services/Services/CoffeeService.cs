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
            if (coffeeToAdd == null) { throw new Exception("You should not add null entries!"); }
            foreach(CoffeeModel coffee in coffeeModels)
            { if (coffee.Id == coffeeToAdd.Id) { throw new Exception("Already exists an entry with the same Id"); } }
            if (!(coffeeToAdd.Id == Guid.Empty || coffeeToAdd.Name == null || coffeeToAdd.Price == 0.0f))
            {
                coffeeModels.Add(coffeeToAdd);
            }
           
        }

        public void DeleteCoffee(Guid coffeeId)
        {
            if (coffeeId == Guid.Empty) { throw new Exception("The Id given is empty"); }
            foreach (CoffeeModel coffee in coffeeModels)
            {
                if (coffee.Id == coffeeId)
                {
                    coffeeModels.Remove(coffee);
                    break;
                }
            }
        }

        public List<CoffeeModel> GetCoffees()
        {
            return coffeeModels;
        }
    }
}
