using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Interfaces
{
    public interface ICoffeeDataService
    {
        Task AddCoffeeData(CoffeeMachineData coffeeMachineData);
        Task<List<CoffeeDataEntity>> GetCoffeeDatas();
        Task DeleteFirstCoffeeData();
    }
}
