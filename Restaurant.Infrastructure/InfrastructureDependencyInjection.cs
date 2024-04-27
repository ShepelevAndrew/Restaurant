using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Abstractions.Auth;
using Restaurant.Domain.Users.Repositories;
using Restaurant.Infrastructure.Authentication;
using Restaurant.Infrastructure.Persistent;
using Restaurant.Infrastructure.Persistent.Repositories;

namespace Restaurant.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDatabase(configuration);
        services.AddAuth(configuration);
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}