﻿using Plurish.Template.Domain.Tempos.Abstractions;
using Plurish.Template.Domain.Tempos.Models;

using Plurish.Template.Infra.Tempos.Dtos.WeatherResponse;

using Microsoft.Extensions.Options;

using Refit;

namespace Plurish.Template.Infra.Tempos.Repositories;

internal sealed class TempoRepository(
    IOpenWeatherApiClient weatherApi,
    IOptionsMonitor<Settings.Api> apis
) : ITempoRepository
{
    readonly IOpenWeatherApiClient _weatherApi = weatherApi;
    readonly string _apiToken = apis.CurrentValue.OpenWeather.Token;

    /// <summary>
    /// Busca o tempo corrente da cidade especificada
    /// </summary>
    /// <param name="cidade"></param>
    /// <returns></returns>
    /// <exception cref="ApiException">Lançada quando a API não retorna status de sucesso</exception>
    public async Task<Tempo?> BuscarTempoAtual(Cidade cidade)
    {
        WeatherResponseDto? response = await _weatherApi.BuscarTempoLocal(
            _apiToken,
            cidade.Id.Latitude,
            cidade.Id.Longitude
        );

        return response?.ParaTempo(cidade);
    }
}