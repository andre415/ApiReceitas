using FluentMigrator;


namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;

[Migration((long)NumeroVersoes.CriarTabelaUsuario, "Usuario")]

public class Version001 : Migration
{

    public override void Down()
    {
       
    }

    public override void Up()
    {

        var tabela = VersionBase.InserirColunasPadrao(Create.Table("Usuario"));

        tabela.WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("Telefone").AsString(14).NotNullable();
       
    }
}
