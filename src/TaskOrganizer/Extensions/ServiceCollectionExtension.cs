﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskOrganizer.AppConfiguration;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Interfaces;
using TaskOrganizer.Repository.Services;
using TaskOrganizer.View.Windows;
using TaskOrganizer.ViewModels;

namespace TaskOrganizer.Extensions;
internal static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
    {
        return services.AddSingleton<MainViewModel>()
            .AddScoped<TaskViewModel>()
            .AddScoped<PomodoroViewModel>()
            .AddScoped<SettingsViewModel>()
            .AddScoped<LoginViewModel>()
            .AddScoped<UserAccountViewModel>()
            .AddScoped<AnalyticsViewModel>();
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfigurationRoot configurationRoot)
    {
        return services.Configure<ConnectionStringOptions>(configurationRoot.GetSection(ConnectionStringOptions.SectionName))
            .Configure<LoggerOptions>(configurationRoot.GetSection(LoggerOptions.SectionName));
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services) 
    {
        return services.AddScoped<IUserService, UserService>()
            .AddScoped<ITaskService, TaskService>()
            .AddScoped<IPomodoroService, PomodoroService>();
    }

    public static IServiceCollection ConfigureWindows(this IServiceCollection services)
    {
       return services.AddScoped<LoginWindow>()
            .AddScoped<MainWindow>();
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, HostBuilderContext builder)
    {
        return services.AddDbContext<TaskOrganizerDbContext>(options =>
            options.UseSqlite(builder.Configuration[$"{ConnectionStringOptions.SectionName}:Default"]),
                ServiceLifetime.Scoped);
    }
}
