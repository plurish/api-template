using Plurish.Common.Abstractions.Domain;
using Plurish.Template.Domain.Tempos.Models.ValueObjects;

namespace Plurish.Template.Domain.Tempos.Models;

public sealed class Cidade(CidadeId id, string nome) : Entity<CidadeId>(id)
{
    public string Nome { get; private set; } = nome;
}