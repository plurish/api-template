using System.Text.Json.Serialization;
using Plurish.Common.Abstractions.Domain.Events;

namespace Plurish.Template.Domain.Tempos;

public sealed record TemperaturaDiminuida(
    [property: JsonPropertyName("tempo_id")]
    int TempoId,

    [property: JsonPropertyName("temperatura_antiga")]
    decimal TemperaturaAntiga,

    [property: JsonPropertyName("temperatura_atual")]
    decimal TemperaturaAtual
) : IDomainEvent;