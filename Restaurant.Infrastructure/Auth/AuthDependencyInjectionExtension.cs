using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Abstractions.Auth;
using Restaurant.Infrastructure.Auth.Authentication;
using Restaurant.Infrastructure.Auth.Authentication.CustomJwtClaims;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;
using Restaurant.Infrastructure.Options;

namespace Restaurant.Infrastructure.Auth;

public static class AuthDependencyInjectionExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();

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

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                AuthPolicy.VerifiedAccount,
                policy => policy.RequireClaim(JwtClaims.EmailVerified, JwtClaims.BoolValueTrue));
        });

        return services;
    }
}