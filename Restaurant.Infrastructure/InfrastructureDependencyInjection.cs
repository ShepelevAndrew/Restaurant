using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Abstractions.VerificationCode;
using Restaurant.Infrastructure.Auth;
using Restaurant.Infrastructure.Options;
using Restaurant.Infrastructure.Persistent;
using Restaurant.Infrastructure.VerificationCodeServices;

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
            .AddScoped<ICodeSender, EmailCodeSender>()
            .AddScoped<ICodeGenerator, CodeGenerator>();

        return services;
    }
}