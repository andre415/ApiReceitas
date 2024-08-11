namespace MeuLivroDeReceitas.Domain.Repositorio.Usuario;

public interface IRepositorioUsuarioReadOnly
{
    Task<bool> ExisteUsuarioComEmail(string email);

    Task<Entidades.Usuario> Login(string email, string senha);
    Task<Entidades.Usuario> RecuperarPorEmail(string email);
}
