using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Windows;
using System.Windows.Input;
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
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("AppSettings.json")
            .Build();

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((builder, services) =>
            {
                ConfigureServices(builder, services);
                ConfigureOptions(config, services);
            })
        .Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        Log.Information("Starting application...", typeof(App));
        await Host.StartAsync();
        MainWindow mainWindow = Host.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = Host.Services.GetRequiredService<MainViewModel>();
        mainWindow.Show();

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

        services.AddAutoMapper(typeof(App));

        services.AddDbContext<TaskOrganizerDbContext>(options =>
            options.UseSqlite(builder.Configuration[$"{ConnectionStringOptions.SectionName}:Default"]),
            ServiceLifetime.Scoped);

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(builder.Configuration[$"{LoggerOptions.SectionName}:OutputFilepath"],
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
            .CreateLogger();

        services.BuildServiceProvider();
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddScoped<MainViewModel>()
            .AddScoped<TodoViewModel>()
            .AddScoped<PomodoroViewModel>()
            .AddScoped<SettingsViewModel>();
    }

    private static void ConfigureOptions(IConfigurationRoot configurationRoot, IServiceCollection services)
    {
        services.Configure<ConnectionStringOptions>(configurationRoot.GetSection(ConnectionStringOptions.SectionName))
            .Configure<LoggerOptions>(configurationRoot.GetSection(LoggerOptions.SectionName));
    }

    private static void ConfigureStorages(IServiceCollection services) { }
}
