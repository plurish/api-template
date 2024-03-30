﻿using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Plurish.Template.Application.Tempos.Abstractions;
using Plurish.Template.Application.Tempos.Services;

namespace Plurish.Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddMappers()
            .AddServices();

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Default
            .MapToConstructor(true);

        return services
            .AddSingleton(TypeAdapterConfig.GlobalSettings)
            .AddSingleton<IMapper, ServiceMapper>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddMediatR(config =>
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
            )
            .AddSingleton<ITempoService, TempoService>()
            .AddSingleton<ICidadeService, CidadeService>();
}