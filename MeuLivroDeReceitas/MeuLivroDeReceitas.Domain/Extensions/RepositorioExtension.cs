using Microsoft.Extensions.Configuration;

namespace MeuLivroDeReceitas.Domain.Extensions;

public static class RepositorioExtension
{
    public static string GetnomeDataBase(this IConfiguration configurationManager )
    {
        var nomeDB = configurationManager.GetConnectionString("NomeDataBase");

        return nomeDB;
    }

    public static string GetConexao(this IConfiguration configurationManager)
    {
        var conexao = configurationManager.GetConnectionString("Conexao");
        return conexao;
    }

    public static string GetConaxaoCompleta(this IConfiguration configurationManager)
    {
        var nomeDB = configurationManager.GetnomeDataBase();
        var conexao = configurationManager.GetConexao();
        return $"{conexao}DataBase={nomeDB}";
    }
}
