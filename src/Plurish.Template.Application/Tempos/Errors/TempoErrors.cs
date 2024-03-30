using Plurish.Common.Types.Output;
using Plurish.Template.Domain.Tempos.Dtos;

namespace Plurish.Template.Application.Tempos.Errors;

internal static class TempoErrors
{
    internal static readonly Result<TempoDto?> CidadeInvalida =
        Result<TempoDto?>.InvalidInput(["Determine a cidade cujo clima será buscado"]);

    internal static readonly Result<TempoDto?> TempoNaoEncontrado =
        Result<TempoDto?>.UnexpectedError(["Algum erro ocorreu ao tentar buscar o tempo"]);
}