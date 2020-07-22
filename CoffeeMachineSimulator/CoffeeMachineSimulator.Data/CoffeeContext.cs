using CoffeeMachineSimulator.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachineSimulator.Data
{
    public class CoffeeContext : DbContext
    {
        public DbSet<CoffeeEntity> Coffees { get; set; }
        public DbSet<EspressoMachineEntity> EspressoMachines { get; set; }

        public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureCoffeeModel(modelBuilder);
            ConfigureEspressoMachineModel(modelBuilder);
        }

        private static void ConfigureCoffeeModel(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CoffeeEntity>();
            entity.ToTable("Coffees");
            entity.HasOne(x => x.EspressoMachine).WithMany();
        }

        private static void ConfigureEspressoMachineModel(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EspressoMachineEntity>();
            entity.ToTable("EspressoMachines");
        }
    }
}
