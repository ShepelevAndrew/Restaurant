using System.Reflection;
using Microsoft.OpenApi.Models;
using Restaurant.Common.Mapping;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Restaurant;

public static class PresentationDependencyInjection
{
    public static void UseSerilog(this IHostBuilder builder)
    {
        builder.UseSerilog((context, config) =>
        {
            var elasticSearchConn = new Uri(context.Configuration.GetConnectionString("ElasticSearch")!);

            var defaultLogging = config.Enrich.FromLogContext()
                                                    .Enrich.WithMachineName();

            if(context.HostingEnvironment.IsProduction())
            {
                var envName = context.HostingEnvironment.EnvironmentName.ToLower().Replace(".", "-");

                defaultLogging.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticSearchConn)
                {
                    IndexFormat = $"restaurant-logs-{envName}-{DateTime.UtcNow:yyyy-MM}",
                    AutoRegisterTemplate = true,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                });
            }

            defaultLogging.WriteTo.Console()
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
        });
    }

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

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}