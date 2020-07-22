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

namespace CoffeeMachineSimulator.UI
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddScoped<MainViewModel>();
            services.AddScoped<ICoffeeService, CoffeeService>();
            services.AddScoped<ICoffeMachineDataSender, CoffeeMachineDataSender>();

            services.AddAutoMapper(GetType().Assembly);
        }
    }
}
