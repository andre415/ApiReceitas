using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;

[Migration((long)NumeroVersoes.CriarTabelaDeReceitas, "CriarTabelaDeReceitas")]
public class Version002 : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {

        CriarTabelaReceitas();
        CriarTabelaIngredientes();
    }

    private void CriarTabelaReceitas()
    {
        var tabela = VersionBase.InserirColunasPadrao(Create.Table("Receitas"));

        tabela.WithColumn("Titulo").AsString(100).NotNullable()
            .WithColumn("Categoria").AsInt16().NotNullable()
            .WithColumn("ModoDePreparo").AsString(5000).NotNullable()
             .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("Fk_Receita_Usuario_Id", "usuario", "Id");
    }
    private void CriarTabelaIngredientes()
    {
        var tabela = VersionBase.InserirColunasPadrao(Create.Table("Ingredientes"));

        tabela.WithColumn("Produto").AsString(100).NotNullable()
            .WithColumn("Quantidade").AsString(5000).NotNullable()
            .WithColumn("ReceitaId1").AsInt64().Nullable()
            .WithColumn("ReceitaId").AsInt64().Nullable().ForeignKey("Fk_Ingredientes_Receita_Id", "Receitas", "Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);
    }
}
