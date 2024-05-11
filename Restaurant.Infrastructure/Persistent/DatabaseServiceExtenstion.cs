using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Common.Abstractions.Users;
using Restaurant.Domain.Orders.Repositories;
using Restaurant.Domain.Products.Repositories;
using Restaurant.Domain.Users.Repositories;
using Restaurant.Infrastructure.Persistent.Repositories;

namespace Restaurant.Infrastructure.Persistent;

public static class DatabaseServiceExtenstion
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfigurationManager configuration)
    {
        services
            .AddSqlDb(configuration)
            .AddCache(configuration)
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserManager, UserManager>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IPermissionRepository, PermissionRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IOrderDetailRepository, OrderDetailRepository>();

        return services;
    }

    private static IServiceCollection AddSqlDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");

        services.AddDbContext<RestaurantDbContext>(
            options => options.UseNpgsql(connection));

        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            var connection = configuration.GetConnectionString("Cache");
            options.Configuration = connection;
        });

        return services;
    }
}