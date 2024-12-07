using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskOrganizer.AppConfiguration;
using TaskOrganizer.Repository.Interfaces;
using TaskOrganizer.Repository.Services;
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
            .AddScoped<UserAccountViewModel>();
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfigurationRoot configurationRoot)
    {
        return services.Configure<ConnectionStringOptions>(configurationRoot.GetSection(ConnectionStringOptions.SectionName))
            .Configure<LoggerOptions>(configurationRoot.GetSection(LoggerOptions.SectionName));
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services) 
    {
        return services.AddScoped<IUserService, UserService>();
    }
}
