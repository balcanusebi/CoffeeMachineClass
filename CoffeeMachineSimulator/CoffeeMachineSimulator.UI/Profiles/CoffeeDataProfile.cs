using AutoMapper;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Sender.Model.CoffeeMachine.Simulator.Sender.Model;

namespace CoffeeMachineSimulator.UI.Profiles
{
    public class CoffeeDataProfile : Profile
    {
        public CoffeeDataProfile()
        {
            CreateMap<CoffeeMachineData, CoffeeDataEntity>().ReverseMap();
        }
    }
}
