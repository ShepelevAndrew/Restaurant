using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Common.Abstractions.Auth;
using Restaurant.Application.Common.Abstractions.Auth.VerificationCode;
using Restaurant.Infrastructure.Auth.Authentication;
using Restaurant.Infrastructure.Auth.Authentication.CustomJwtClaims;
using Restaurant.Infrastructure.Auth.Authorization;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;
using Restaurant.Infrastructure.Auth.VerificationCodeServices;
using Restaurant.Infrastructure.Options;

namespace Restaurant.Infrastructure.Auth;

public static class AuthDependencyInjectionExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IJwtProvider, JwtProvider>()
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>()
            .AddAuthentication(configuration)
            .AddAuthorization(configuration)
            .AddScoped<ICodeSender, EmailCodeSender>()
            .AddScoped<ICodeGenerator, CodeGenerator>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                AuthPolicy.VerifiedAccount,
                policy => policy.RequireClaim(JwtClaims.EmailVerified, JwtClaims.BoolValueTrue));
        });

        return services;
    }
}