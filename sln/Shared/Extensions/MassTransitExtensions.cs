﻿using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions;
public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitWithAssemblies
        (this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            config.SetInMemorySagaRepositoryProvider();

            config.AddConsumers(assemblies);
            config.AddSagaStateMachines(assemblies);
            config.AddSagas(assemblies);
            config.AddActivities(assemblies);

            config.UsingInMemory((context, configurator) =>
            {
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
