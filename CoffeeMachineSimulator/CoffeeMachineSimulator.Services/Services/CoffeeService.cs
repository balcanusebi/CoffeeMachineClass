using AutoMapper;
using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Data.Entities;
using CoffeeMachineSimulator.Services.Interfaces;
using CoffeeMachineSimulator.Services.Models;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineSimulator.Services.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly List<CoffeeModel> coffeeModels;
        private readonly string connectionstring = "Server=DESKTOP-FCS0D3H\\SBALCANU;Integrated Security=true; Database=CofeeDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        private readonly IMapper mapper;

        public CoffeeService(IMapper mapper)
        {
            this.mapper = mapper;
            coffeeModels = Builder<CoffeeModel>.CreateListOfSize(10)
                .TheFirst(1)
                .With(x=>x.Name = "Lavazza")
                .Build()
                .ToList();
        }

        public void AddCoffee(CoffeeModel coffeeToAdd)
        {
            if (coffeeToAdd == null) throw new Exception("You should not add null entries!");
            if (!IsCoffeeValid(coffeeToAdd)) throw new Exception("The coffee you are trying to add is not valid");

            coffeeModels.Add(coffeeToAdd);
            SaveCoffeeToDb(coffeeToAdd);
        }

        public void DeleteCoffee(Guid coffeeId)
        {
            if(coffeeId == Guid.Empty || coffeeId == null) throw new Exception("Please provide an ID!");

            var coffeeFromList = coffeeModels.FirstOrDefault(x => x.Id == coffeeId);
            if (coffeeFromList == null) throw new Exception("The coffee you are trying to delete does not exist!");

            coffeeModels.Remove(coffeeFromList);
        }

        public List<CoffeeModel> GetCoffees()
        {
            return coffeeModels;
        }

        private bool IsCoffeeValid(CoffeeModel model)
        {
            return model.Id != Guid.Empty && !string.IsNullOrEmpty(model.Name) && model.Price != 0.0f;
        }

        private void SaveCoffeeToDb(CoffeeModel coffeeModel)
        {
            var coffeeEntityToAdd = mapper.Map<CoffeeEntity>(coffeeModel);

            var optionsBuilder = new DbContextOptionsBuilder<CoffeeContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            CoffeeContext dbContext = new CoffeeContext(optionsBuilder.Options);

            dbContext.Coffees.Add(coffeeEntityToAdd);
            dbContext.Coffees.Remove(coffeeEntityToAdd);
            var entityFromDb = dbContext.Coffees.FirstOrDefault(x => x.Id == Guid.NewGuid());

            entityFromDb.Name = "My new name";

            dbContext.SaveChanges();
        }
    }
}
