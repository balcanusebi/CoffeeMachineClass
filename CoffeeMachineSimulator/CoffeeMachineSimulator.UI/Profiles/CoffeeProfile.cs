using AutoMapper;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Services.Models;

namespace CoffeeMachineSimulator.UI.Profiles
{
    public class CoffeeProfile : Profile
    {
        public CoffeeProfile()
        {
            CreateMap<CoffeeModel, CoffeeEntity>()
                .ForMember(dest => dest.Sweetness, opts => opts.MapFrom(x => x.Sweetness))
                .ForMember(dest => dest.EspressoMachine, opts => opts.Ignore());

            CreateMap<CoffeeEntity, CoffeeModel>()
                .ForMember(dest => dest.IsEsspreso, opts => opts.Ignore())
                .ForMember(dest => dest.Sweetness, opts => opts.MapFrom(x=>x.Sweetness));
        }
    }
}
