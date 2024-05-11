using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Common.Abstractions.NotificationSenders;
using Restaurant.Infrastructure.Auth;
using Restaurant.Infrastructure.Options;
using Restaurant.Infrastructure.Persistent;
using Restaurant.Infrastructure.WebSocketHubs;

namespace Restaurant.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services
            .AddDatabase(configuration)
            .AddAuth(configuration)
            .AddOptions(configuration)
            .AddScoped<INotificationSender, NotificationSender>();

        return services;
    }
}