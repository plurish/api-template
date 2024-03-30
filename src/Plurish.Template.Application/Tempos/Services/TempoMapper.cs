using Plurish.Template.Domain.Tempos.Dtos;
using Plurish.Template.Domain.Tempos.Models;
using Plurish.Template.Domain.Tempos.Models.ValueObjects;
using Plurish.Common.Types.Output;

namespace Plurish.Template.Application.Tempos.Services;

internal static class TempoMapper
{
    /// <summary>
    /// De DTO para Domain object
    /// </summary>
    public static Tempo ParaDomain(this TempoDto dto)
    {
        Temperatura temperatura = Temperatura.Criar(dto.Temperatura.Celsius)!;
        Temperatura sensacaoTermica = Temperatura.Criar(dto.SensacaoTermica.Celsius)!;

        Cidade cidade = new(
            new CidadeId(dto.Cidade.Id.Latitude, dto.Cidade.Id.Longitude),
            dto.Cidade.Nome
        );

        return new(
            dto.Id,
            dto.Descricao,
            temperatura,
            sensacaoTermica,
            dto.Humidade,
            cidade
        );
    }
}