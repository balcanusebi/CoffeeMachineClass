using CoffeeMachineSimulator.Services.Enums;
using System;

namespace CoffeeMachineSimulator.Services.Models
{
    public class CoffeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SweetnessEnum Sweetness { get; set; }
        public float Price { get; set; }
        public EspressoMachineModel EspressoMachine { get; set; }
    }
}
