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
            var espressoMachineId1 = Guid.NewGuid();
            var espressoMachineId2 = Guid.NewGuid();
            var espressoMachineId3 = Guid.NewGuid();
            coffeeModels = Builder<CoffeeModel>.CreateListOfSize(10)
                .TheFirst(3)
                    .With(x=>x.Name = "Lavazza")
                    .With(x=>x.EspressoMachine = GenerateNewEspressoMachine("De'Longhi", espressoMachineId1))
                .TheNext(3)
                    .With(x => x.Name = "Doppio")
                    .With(x => x.EspressoMachine = GenerateNewEspressoMachine("Philips", espressoMachineId2))
                    .With(x => x.Sweetness=Enums.SweetnessEnum.Bitter)
                .TheLast(4)
                    .With(x => x.Name = "Red Eye")
                    .With(x => x.EspressoMachine = GenerateNewEspressoMachine("Bosch Tassimo", espressoMachineId3))
                    .With(x => x.Filled = true)
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

        public EspressoMachineModel GenerateNewEspressoMachine(string name, Guid espressoMachineId)
        {
            var generatedEspressoMachine = Builder<EspressoMachineModel>.CreateNew()
                    .With(x=>x.Name=name)
                    .With(x=>x.Id=espressoMachineId)
                .Build();
            return generatedEspressoMachine;
        }

        public void DrinkCoffee(Guid coffeeId)
        {
            if (coffeeId == Guid.Empty || coffeeId == null) throw new Exception("Please provide an ID!");
            if (coffeeModels.FirstOrDefault(x => x.Id== coffeeId).Filled == false) throw new Exception("The coffee you are trying to drink has already been drank!");
            coffeeModels.FirstOrDefault(x => x.Id == coffeeId).Filled = false;
        }

        public void UpdateCoffee(CoffeeModel coffeeToUpdate)
        {
            if (coffeeToUpdate == null) throw new Exception("The coffee you provided is null");
            if (!IsCoffeeValid(coffeeToUpdate)) throw new Exception("The coffee you provided is not valid");
            if (coffeeModels.All(x => x.Id != coffeeToUpdate.Id)) throw new Exception("There is no coffee with the ID you provided!");
            coffeeModels.FirstOrDefault(x => x.Id == coffeeToUpdate.Id).Name = coffeeToUpdate.Name;
            coffeeModels.FirstOrDefault(x => x.Id == coffeeToUpdate.Id).Price = coffeeToUpdate.Price;
            coffeeModels.FirstOrDefault(x => x.Id == coffeeToUpdate.Id).Filled = coffeeToUpdate.Filled;
            coffeeModels.FirstOrDefault(x => x.Id == coffeeToUpdate.Id).EspressoMachine = coffeeToUpdate.EspressoMachine;
            coffeeModels.FirstOrDefault(x => x.Id == coffeeToUpdate.Id).Sweetness = coffeeToUpdate.Sweetness;
        }

        public List<CoffeeModel> GetAllCoffeesFromEspressoMachine(Guid espressoMachineId)
        {
            if (espressoMachineId == Guid.Empty || espressoMachineId == null) throw new Exception("Please provide an ID!");
            if (coffeeModels.All(x => x.EspressoMachine.Id != espressoMachineId)) throw new Exception("There is no espresso machine with that ID");
            
            var coffeesFromEspresso = new List<CoffeeModel>();
            foreach(CoffeeModel coffee in coffeeModels)
            {
                if (coffee.EspressoMachine.Id == espressoMachineId)
                    coffeesFromEspresso.Add(coffee);
            }

            return coffeesFromEspresso;
        }
    }
}
