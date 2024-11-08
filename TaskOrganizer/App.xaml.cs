using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.EFCore;
using TaskOrganizer.EFCore.Services;
using TaskOrganizer.Helper;
using TaskOrganizer.Store;
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
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    ConfigureServices(services);
            })
            .Build();
        }
        public readonly IHost Host;
        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host.StartAsync();
            MainWindow mainWindow = Host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            mainWindow.DataContext = Host.Services.GetRequiredService<MainViewModel>();
            MyDbContextFactory DbContext = Host.Services.GetRequiredService<MyDbContextFactory>();
            IDataService<TaskModel> taskService = Host.Services.GetRequiredService<IDataService<TaskModel>>();
            DbContext.CreateDbContext();
            await taskService.Get(7);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (Host)
            {
                await Host.StopAsync();
            }
            base.OnExit(e);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MyDbContextFactory>();
            services.AddSingleton<IDataService<TaskModel>, GenericDataService<TaskModel>>();
            services.AddSingleton<IDataService<PomodoroModel>, GenericDataService<PomodoroModel>>();

            services.AddScoped<MainViewModel>();
            services.AddScoped<TodoViewModel>();
            services.AddScoped<PomodoroViewModel>();
            services.AddScoped<SettingsViewModel>();

            services.AddScoped<PomodoroStore>();
            services.AddScoped<TodoStore>();
            services.AddScoped<UpdateViewCommand>();

            services.AddAutoMapper(typeof(App));
            
            services.BuildServiceProvider();
        }
    }
}
