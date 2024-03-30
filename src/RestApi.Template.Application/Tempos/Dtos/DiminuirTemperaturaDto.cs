﻿using System.Text.Json.Serialization;

namespace RestApi.Template.Application.Tempos.Dtos;

public readonly record struct DiminuirTemperaturaDto(
    string Cidade,

    [property: JsonPropertyName("celsius_diminuidos")]
    decimal CelsiusDiminuidos
);