using CoffeeMachineSimulator.Data.Enums;
using System;

namespace CoffeeMachineSimulator.Data.Entities
{
    public class CoffeeEntity : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Sweetness Sweetness { get; set; }
        public EspressoMachineEntity EspressoMachine { get; set; }
        public Guid EspressoMachineId { get; set; }
    }
}
