﻿using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}