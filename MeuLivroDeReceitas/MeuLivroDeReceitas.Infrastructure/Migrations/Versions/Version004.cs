using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;

[Migration((long)NumeroVersoes.CriarTabelaAssociacaoUsuario, "Adicionado tabelas para associação de usuarios")]
public class Version004 : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        CriarTabelaCodigo();
        CriarTabelaConexao();
    }

    private void CriarTabelaConexao()
    {
        var tabela = VersionBase.InserirColunasPadrao(Create.Table("Conexao"));
        
        tabela
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("Fk_Conexao_Usuario_Id", "usuario", "Id")
            .WithColumn("ConectadoComUsuarioId").AsInt64().NotNullable().ForeignKey("Fk_Conexao_ConectadoComUsuario_Id", "usuario", "Id");

    }

    private void CriarTabelaCodigo()
    {
        var tabela = VersionBase.InserirColunasPadrao(Create.Table("Codigos"));

        tabela
            .WithColumn("Codigo").AsString(2000).NotNullable()
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("Fk_Codigo_Usuario_Id", "usuario", "Id");
    }
}
