using CoffeeMachineSimulator.Services.Enums;
using CoffeeMachineSimulator.Services.Interfaces;
using CoffeeMachineSimulator.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineSimulator.Services.Services
{
    public class EspressoMachineService : IEspressoMachineService
    {
        public ICoffeeService coffeeService;

        public EspressoMachineService(ICoffeeService coffeeService)
        {
            this.coffeeService = coffeeService;
        }

        public float GetSumOfAllCoffees()
        {
            var coffees = coffeeService.GetCoffees();

            return coffees.Sum(i=>i.Price);
        }

        public CoffeeModel GiveMeACoffee(SweetnessEnum sweetness)
        {
            var listOfCoffees = coffeeService.GetCoffees();
            var myCoffeeToReturn = listOfCoffees.FirstOrDefault(x=>x.Sweetness == sweetness);
            myCoffeeToReturn.Name = "TestMyCoffee";

            return myCoffeeToReturn;
        }

        public List<CoffeeModel> MakeAllCoffeesWithSweetness(SweetnessEnum sweetness)
        {
            var listOfCoffees = coffeeService.GetCoffees();

            foreach (CoffeeModel coffeeModel in listOfCoffees)
            {
                coffeeModel.Sweetness = sweetness;
            }
            return listOfCoffees;
        }
    }
}
