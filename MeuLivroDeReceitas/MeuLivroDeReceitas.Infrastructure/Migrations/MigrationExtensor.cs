using FluentMigrator;
using FluentMigrator.Runner;
using MeuLivroDeReceitas.Infrastructure.Migrations.Versions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class MigrationExtensor
{
    public static void MigrarBancoDeDados( this IApplicationBuilder app)
    {
        
        using var scope = app.ApplicationServices.CreateScope();
        var runner  = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
        
    }
}