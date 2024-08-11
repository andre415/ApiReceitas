using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;
[Migration((long)NumeroVersoes.AlterarTabelaDeReceitas, "CriarColulaTempoDePreparo")]
public class Version003 : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Alter.Table("receitas").AddColumn("TempoDePreparo").AsInt32().NotNullable().WithDefaultValue(0);
    }

}