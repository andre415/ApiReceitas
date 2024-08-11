using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Domain.Repositorio.Conexao;
public interface IConexaoWriteOnly
{
    Task Registrar(Entidades.Conexao conexao);
}
