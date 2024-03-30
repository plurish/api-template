﻿using RestApi.Template.Domain.Tempos.Models.ValueObjects;
using RestApi.Common.Abstractions.Domain;

namespace RestApi.Template.Domain.Tempos.Models;

public sealed class Cidade(CidadeId id, string nome) : Entity<CidadeId>(id)
{
    public string Nome { get; private set; } = nome;
}