using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TaskOrganizer.Domain;
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
            ConfigureViewModels(services);
            services.AddSingleton<MainWindow>();
            services.AddScoped<UpdateViewCommand>();

            services.AddAutoMapper(typeof(App));

            services.AddDbContext<TaskOrganizerDbContext>();

            services.BuildServiceProvider();
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<TodoViewModel>();
            services.AddScoped<PomodoroViewModel>();
            services.AddScoped<SettingsViewModel>();
        }

        private static void ConfigureStorages(IServiceCollection services)
        {

        }
    }
}
