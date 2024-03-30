using Plurish.Template.Domain.Tempos.Models.ValueObjects;
using Plurish.Common.Abstractions.Domain;

namespace Plurish.Template.Domain.Tempos.Models;

public sealed class Cidade(CidadeId id, string nome) : Entity<CidadeId>(id)
{
    public string Nome { get; private set; } = nome;
}