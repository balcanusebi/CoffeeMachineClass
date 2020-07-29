using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using System;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Interfaces
{
    public interface ICoffeeDataService
    {
        Task AddCoffeeData(CoffeeMachineData coffeeMachineData);
        Task MultiplyCoffee(Guid coffeeId, int times);
    }
}
