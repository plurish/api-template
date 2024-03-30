using Plurish.Common.Types.Output;
using Plurish.Template.Domain.Tempos.Dtos;

namespace Plurish.Template.Application.Tempos.Abstractions;

public interface ICidadeService
{
    Task<Result<CidadeDto?>> BuscarPorNome(string cidade);
}