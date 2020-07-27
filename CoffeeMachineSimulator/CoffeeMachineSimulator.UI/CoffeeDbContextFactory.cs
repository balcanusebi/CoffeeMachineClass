using CoffeeMachineSimulator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeMachineSimulator.UI
{
    public class CoffeeDbContextFactory : IDesignTimeDbContextFactory<CoffeeContext>
    {
        public CoffeeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoffeeContext>();
            var connectionString = "Server=DESKTOP-FCS0D3H\\SBALCANU;Integrated Security=true; Database=CofeeDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("CoffeeMachineSimulator.Data"));
            
            return new CoffeeContext(optionsBuilder.Options);
        }
    }
}
