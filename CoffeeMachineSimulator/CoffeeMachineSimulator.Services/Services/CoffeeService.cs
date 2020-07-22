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
            coffeeModels = Builder<CoffeeModel>.CreateListOfSize(10)
                .TheFirst(1)
                    .With(x=>x.Name = "Lavazza")
                    .With(x=>x.EspressoMachine = GenerateNewEspressoMachine("espresso machine name"))
                .Build()
                .ToList();
        }

        public void AddCoffee(CoffeeModel coffeeToAdd)
        {
            if (coffeeToAdd == null) throw new Exception("You should not add null entries!");
            if (!IsCoffeeValid(coffeeToAdd)) throw new Exception("The coffee you are trying to add is not valid");

            coffeeModels.Add(coffeeToAdd);
        }

        public void DeleteCoffee(Guid coffeeId)
        {
            if(coffeeId == Guid.Empty || coffeeId == null) throw new Exception("Please provide an ID!");

            var coffeeFromList = coffeeModels.FirstOrDefault(x => x.Id == coffeeId);
            if (coffeeFromList == null) throw new Exception("The coffee you are trying to delete does not exist!");

            coffeeModels.Remove(coffeeFromList);
        }

        public List<CoffeeModel> GetCoffees()
        {
            return coffeeModels;
        }

        private bool IsCoffeeValid(CoffeeModel model)
        {
            return model.Id != Guid.Empty && !string.IsNullOrEmpty(model.Name) && model.Price != 0.0f;
        }
    }
}
