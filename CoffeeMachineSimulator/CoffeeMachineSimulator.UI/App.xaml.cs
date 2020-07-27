using CoffeeMachineSimulator.Data;
using CoffeeMachineSimulator.Services.Interfaces;
using CoffeeMachineSimulator.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using AutoMapper;
using CoffeeMachineSimulator.UI.ViewModel;
using CoffeeMachineSimulator.Implementation.Sender;
using CoffeeMachineSimulator.Interfaces.Sender;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachineSimulator.UI
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory());

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoffeeContext>
                (options => options.UseSqlServer("Server=DESKTOP-FCS0D3H\\SBALCANU;Integrated Security=true; Database=CofeeDb;Trusted_Connection=True;MultipleActiveResultSets=true"));
            
            services.AddTransient(typeof(MainWindow));
            services.AddSingleton<MainViewModel>();

            services.AddScoped<ICoffeeService, CoffeeService>();
            services.AddScoped<ICoffeMachineDataSender, CoffeeMachineDataSender>();
            services.AddScoped<IEspressoMachineService, EspressoMachineService>();
            services.AddScoped<ICoffeeDataService, CoffeeDataService>();

            services.AddAutoMapper(GetType().Assembly);
        }
    }
}
