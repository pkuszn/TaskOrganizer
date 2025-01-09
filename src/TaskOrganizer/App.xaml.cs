using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Windows;
using TaskOrganizer.AppConfiguration;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Extensions;
using TaskOrganizer.Repository;
using TaskOrganizer.View.Windows;
using TaskOrganizer.ViewModels;

namespace TaskOrganizer;

public partial class App : Application
{
    private readonly IHost Host;
    public static IServiceProvider Services { get; private set; }
    public App()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((builder, services) =>
            {
                IHostEnvironment env = builder.HostingEnvironment;
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile($"AppSettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File(builder.Configuration[$"{LoggerOptions.SectionName}:OutputFilepath"],
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true)
                    .CreateLogger();

                services.AddSingleton(Log.Logger)
                    .ConfigureServices()
                    .ConfigureViewModels()
                    .ConfigureWindows()
                    .AddAutoMapper(typeof(App))
                    .ConfigureDbContext(builder)
                    .ConfigureOptions(config)
                    .BuildServiceProvider();
            })
            .UseSerilog()
            .Build();

        Services = Host.Services;
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        Log.Information("*** Starting application ***", typeof(App));
        using (IServiceScope scope = Host.Services.CreateScope())
        {
            TaskOrganizerDbContext dbContext = scope.ServiceProvider.GetRequiredService<TaskOrganizerDbContext>();
            IHostEnvironment environment = Host.Services.GetRequiredService<IHostEnvironment>();
            if (environment.IsDevelopment())
            {
                //TODO: temp
                List<User> users = [.. dbContext.Users];
                foreach (User user in users)
                {
                    Log.Debug($"User: {user.Login}");
                }
            }
            Log.Debug("*** Database setup completed ***");
        }

        LoginWindow loginWindow = Host.Services.GetRequiredService<LoginWindow>();
        loginWindow.DataContext = Host.Services.GetRequiredService<LoginViewModel>();
        loginWindow.Show();

        await Host.StartAsync();
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
}
