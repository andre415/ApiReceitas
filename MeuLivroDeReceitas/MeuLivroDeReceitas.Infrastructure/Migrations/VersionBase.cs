using FluentMigrator.Builders.Create.Table;

namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class VersionBase
{
    public static ICreateTableWithColumnSyntax InserirColunasPadrao(ICreateTableWithColumnSyntax tabela)
    {
        return tabela.WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("DataDeCriacao").AsDateTime().NotNullable();
    }
}
