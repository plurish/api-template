using System.Text.Json.Serialization;

namespace Plurish.Template.Application.Tempos.Dtos;

public readonly record struct DiminuirTemperaturaDto(
    string Cidade,

    [property: JsonPropertyName("celsius_diminuidos")]
    decimal CelsiusDiminuidos
);