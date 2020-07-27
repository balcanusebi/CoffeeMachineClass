using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Data.Enums;
using CoffeeMachineSimulator.UI.Profiles;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeMachineSimulator.Tests
{
    public class ServiceSetUp
    {
        protected static readonly Guid CoffeeEspressorId = Guid.NewGuid();
        protected static readonly Guid EspressorEspressorId = Guid.NewGuid();
        protected readonly Mapper Mapper;
        protected CoffeeContext Context { get; private set; }

        public ServiceSetUp()
        {
            SetUpDatabase();
            Mapper = GetMapperConfig();
        }

        protected void SetUpDatabase()
        {
            var options = new DbContextOptionsBuilder<CoffeeContext>()
                    .UseInMemoryDatabase(databaseName: "CoffeeDb")
                    .Options;

            Context = new CoffeeContext(options);

            Context.EspressoMachines.Add(new EspressoMachineEntity { Id = CoffeeEspressorId, IsEspressor = false });
            Context.EspressoMachines.Add(new EspressoMachineEntity { Id = EspressorEspressorId, IsEspressor = true });

            Context.Coffees.Add(new CoffeeEntity { Name = "First Coffee", Price = 20, Sweetness = Sweetness.Bitter, EspressoMachineId = CoffeeEspressorId });
            Context.Coffees.Add(new CoffeeEntity { Name = "Second Coffee", Price = 30, Sweetness = Sweetness.Sweet, EspressoMachineId = EspressorEspressorId });
            Context.Coffees.Add(new CoffeeEntity { Name = "Third Coffee", Price = 45, Sweetness = Sweetness.LessSweet, EspressoMachineId = EspressorEspressorId });

            Context.SaveChanges();
        }

        private Mapper GetMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoffeeProfile>();
                cfg.AddProfile<CoffeeDataProfile>();
            });

            return new Mapper(config);
        }
    }
}
