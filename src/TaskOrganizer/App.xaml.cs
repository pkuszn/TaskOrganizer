using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TaskOrganizer.AppConfiguration;
using TaskOrganizer.Commands;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Interfaces;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer;

public partial class App : Application
{
    private readonly IHost Host;
    public App()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
        .Build();
    }
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

    private static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
    {
        ConfigureViewModels(services);
        ConfigureStorages(services);

        services.AddSingleton<MainWindow>();
        services.AddScoped<UpdateViewCommand>();

        services.AddAutoMapper(typeof(App));
        
        services.AddDbContext<TaskOrganizerDbContext>(options =>
            options.UseSqlite(builder.Configuration[$"{ConnectionStringOptions.SectionName}:Default"]),
            ServiceLifetime.Scoped);

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.BuildServiceProvider();
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddScoped<MainViewModel>()
            .AddScoped<TodoViewModel>()
            .AddScoped<PomodoroViewModel>()
            .AddScoped<SettingsViewModel>();
    }

    private static void ConfigureStorages(IServiceCollection services)
    {

    }
}
