using System.Text.Json.Serialization;

namespace Plurish.Template.Domain.Tempos.Dtos;

public sealed record TempoDto(
    int Id,
    string Descricao,
    TemperaturaDto Temperatura,
    [property: JsonPropertyName("sensacao_termica")] TemperaturaDto SensacaoTermica,
    int Humidade,
    CidadeDto Cidade
);