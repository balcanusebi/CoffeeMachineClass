using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Services.Enums;
using CoffeeMachineSimulator.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Services.Interfaces
{
    public interface IEspressoMachineService
    {
        void AddEspressoMachine(EspressoMachineEntity espressoMachine);
        Task<EspressoMachineEntity> GetEspressoMachine(bool isEspressoMachine);
    }
}
