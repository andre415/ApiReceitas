using Dapper;
using MySqlConnector;
namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class DataBase
{
    public static void CriarDataBase(string coneçãoComBancoDeDados,string nomeDataBase)
    {
       using var minhaConexão = new MySqlConnection(coneçãoComBancoDeDados);

        var parametros = new DynamicParameters();
        parametros.Add("nome", nomeDataBase);

        var registro = minhaConexão.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome",parametros);

        if (!registro.Any())
        {
            minhaConexão.Query($"CREATE DATABASE {nomeDataBase}");
        }


    }
}

