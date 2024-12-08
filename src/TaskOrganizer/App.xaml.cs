using Microsoft.EntityFrameworkCore;
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
    public App()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(config =>
            {
                string environment = Environment.GetEnvironmentVariable("TASKORGANIZER_ENVIRONMENT") ?? "Development";
                config.AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ASPNETCORE_ENVIRONMENT", environment }
                });
            })
            .ConfigureServices((builder, services) =>
            {
                IHostEnvironment env = builder.HostingEnvironment;
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"AppSettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File(builder.Configuration[$"{LoggerOptions.SectionName}:OutputFilepath"],
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true)
                    .CreateLogger();

                services.AddSingleton(Log.Logger);

                services.ConfigureServices();

                services.ConfigureViewModels();

                services.AddScoped<LoginWindow>();

                services.AddAutoMapper(typeof(App));

                services.AddDbContext<TaskOrganizerDbContext>(options =>
                    options.UseSqlite(builder.Configuration[$"{ConnectionStringOptions.SectionName}:Default"]),
                    ServiceLifetime.Scoped);

                services.ConfigureOptions(config);

                services.BuildServiceProvider();
            })
        .UseSerilog()
        .Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        Log.Information("*** Starting application ***", typeof(App));
        using (IServiceScope scope = Host.Services.CreateScope())
        {
            TaskOrganizerDbContext dbContext = scope.ServiceProvider.GetRequiredService<TaskOrganizerDbContext>();
            IHostEnvironment environment = Host.Services.GetRequiredService<IHostEnvironment>();
            if (environment.IsProduction())
            {
                List<User> users = [.. dbContext.Users];
                foreach (User user in users)
                {
                    Log.Information($"User: {user.Login}");
                }
            }
            Log.Information("*** Database setup completed ***");
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
