﻿using RestApi.Template.Application.Tempos.Abstractions;
using RestApi.Template.Application.Tempos.Errors;
using RestApi.Template.Domain.Tempos.Abstractions;
using RestApi.Template.Domain.Tempos.Dtos;
using RestApi.Template.Domain.Tempos.Models;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using RestApi.Common.Types.Output;

namespace RestApi.Template.Application.Tempos.Services;

internal sealed class CidadeService(
    ICidadeRepository repository,
    IMapper mapper,
    ILogger<CidadeService> logger
) : ICidadeService
{
    readonly ICidadeRepository _repository = repository;
    readonly IMapper _mapper = mapper;
    readonly ILogger<CidadeService> _logger = logger;

    public async Task<Result<CidadeDto?>> BuscarPorNome(string cidade)
    {
        if (string.IsNullOrEmpty(cidade))
        {
            return CidadeErrors.InputInvalido;
        }

        Cidade? entity = await _repository.BuscarPorNome(cidade);

        if (entity is null)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning("A cidade {Cidade} não existe", cidade);
            }

            return CidadeErrors.CidadeInexistente;
        }

        var dto = _mapper.Map<CidadeDto>(entity);

        return Result<CidadeDto?>.Ok(dto);
    }
}