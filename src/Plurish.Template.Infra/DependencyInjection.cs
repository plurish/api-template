﻿using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Plurish.Common.Configuration;
using Plurish.Template.Domain.Tempos.Abstractions;
using Plurish.Template.Infra.Tempos;
using Plurish.Template.Infra.Tempos.Repositories;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Prometheus;
using Refit;

namespace Plurish.Template.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager config
    ) =>
        services
            .AddSettings(
                config,
                out Settings.Api api,
                out Settings.Database db
            )
            .AddHealthChecking(api, db)
            .AddApiClients(api)
            .AddRepositories()
            .AddMappers();

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        ConfigurationManager config,
        out Settings.Api api,
        out Settings.Database db
    )
    {
        services
            .Configure<Settings.Api>(config.GetSection("Api"))
            .Configure<Settings.Database>(config.GetSection("Database"));

        ServiceProvider provider = services.BuildServiceProvider();

        api = provider.GetRequiredService<IOptions<Settings.Api>>().Value;
        db = provider.GetRequiredService<IOptions<Settings.Database>>().Value;

        return services;
    }

    private static IServiceCollection AddHealthChecking(
        this IServiceCollection services,
        Settings.Api apiSettings,
        Settings.Database dbSettings
    )
    {
        services.AddHealthChecks()
            .AddSqlServer(
                name: "Db-Xpto",
                connectionString: dbSettings.Xpto.ConnectionString,
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "mssql"]
            )
            .AddElasticsearch(
                elasticsearchUri: apiSettings.Elasticsearch.Url,
                name: "Elasticsearch",
                failureStatus: HealthStatus.Degraded,
                tags: ["db", "elastic", "log"]
            )
            .ForwardToPrometheus();

        return services;
    }

    private static IServiceCollection AddApiClients(
        this IServiceCollection services,
        Settings.Api apis
    )
    {
        ApiOptions openWeather = apis.OpenWeather;

        services
            .AddRefitClient<IOpenWeatherApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(openWeather.Url);
                c.Timeout = TimeSpan.FromSeconds(openWeather.Timeout);
            })
            .AddTransientHttpErrorPolicy(policyBuilder =>
            {
                IEnumerable<TimeSpan> retryStrategy = Backoff.DecorrelatedJitterBackoffV2(
                    TimeSpan.FromSeconds(openWeather.Resilience.MedianFirstRetryDelay),
                    openWeather.Resilience.RetryCount
                );

                return policyBuilder.WaitAndRetryAsync(retryStrategy);
            });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<ICidadeRepository, CidadeRepository>()
            .AddSingleton<ITempoRepository, TempoRepository>();

    private static IServiceCollection AddMappers(this IServiceCollection services) =>
        services
            .AddSingleton(TypeAdapterConfig.GlobalSettings)
            .AddSingleton<IMapper, ServiceMapper>();
}