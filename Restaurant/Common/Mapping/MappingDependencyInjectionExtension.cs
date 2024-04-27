using System.Reflection;
using Mapster;
using MapsterMapper;

namespace Restaurant.Common.Mapping;

public static class MappingDependencyInjectionExtension
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}