using System.Reflection;
using Microsoft.OpenApi.Models;
using Restaurant.Common.Mapping;

namespace Restaurant;

public static class PresentationDependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenWithInformation();
        services.AddControllers();
        services.AddMapping();

        return services;
    }

    private static void AddSwaggerGenWithInformation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Restaurant API",
                Description = "An ASP.NET Core Web API for managing restaurant services."
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}