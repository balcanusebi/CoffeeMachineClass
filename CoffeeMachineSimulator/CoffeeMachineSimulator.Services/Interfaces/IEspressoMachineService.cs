using CoffeeMachineSimulator.Services.Enums;
using CoffeeMachineSimulator.Services.Models;
using System.Collections.Generic;

namespace CoffeeMachineSimulator.Services.Interfaces
{
    public interface IEspressoMachineService
    {
        CoffeeModel GiveMeACoffee(SweetnessEnum sweetness);
        float GetSumOfAllCoffees();
        List<CoffeeModel> MakeAllCoffeesWithSweetness(SweetnessEnum sweetness);
    }
}
