namespace MeuLivroDeReceitas.Domain.Repositorio.Conexao;
public interface IConexaoReadOnly
{
    Task<bool> ExisteConexao(long usuarioA, long usuarioB);
    Task<IList<Entidades.Usuario>> RecuperarDoUsuario(long idUsuario);
    
}
