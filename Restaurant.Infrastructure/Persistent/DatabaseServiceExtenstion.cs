using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Infrastructure.Persistent;

public static class DatabaseServiceExtenstion
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfigurationManager configuration)
    {
        var connection = configuration.GetConnectionString("RestaurantDb");
        services.AddDbContext<RestaurantDbContext>(
            options => options.UseNpgsql(connection));

        return services;
    }
}