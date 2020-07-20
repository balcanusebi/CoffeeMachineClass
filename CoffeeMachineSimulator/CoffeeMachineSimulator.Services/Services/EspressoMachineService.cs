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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}
