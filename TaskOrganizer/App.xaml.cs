using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.EFCore;
using TaskOrganizer.EFCore.Services;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    ConfigureServices(services);
            })
            .Build();
        }
        public readonly IHost _host;
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            mainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();

            var DbContext = _host.Services.GetRequiredService<MyDbContextFactory>();
            var taskService = _host.Services.GetRequiredService<IDataService<TaskModel>>();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //AddSingleton - creates a single instance throughout the app. It creates a single instance for the first time and reuses the same object in the all calls.
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MyDbContextFactory>();
            services.AddSingleton<IDataService<TaskModel>, GenericDataService<TaskModel>>();
            //AddScopes - It is created once per request within the scope.
            services.AddScoped<MainViewModel>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Creates ServiceProvider containing services from IServiceCollection
            services.BuildServiceProvider();
        }


    }


}
