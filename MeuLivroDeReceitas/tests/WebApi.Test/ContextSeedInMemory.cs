﻿using MeuLivroDeReceitas.Infrastructure.AcessoRepo;
using Utilitario.ParaTestes.Entidades;

namespace WebApi.Test;

public class ContextSeedInMemory
{
    public static (MeuLivroDeReceitas.Domain.Entidades.Usuario usuario, string senha) Seed(MeuLivroDeReceitasContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Construir();

        context.Usuario.Add(usuario);

        context.SaveChanges();

        return (usuario,  senha);
    }
}
