using Plurish.Template.Domain.Tempos.Models;

namespace Plurish.Template.Domain.Tempos.Abstractions;

public interface ICidadeRepository
{
    Task<Cidade?> BuscarPorNome(string cidade);
}