namespace MeuLivroDeReceitas.Domain.Repositorio.Usuario;

public interface IRepositorioUpdate
{
    void Upadate(Entidades.Usuario usuario);
    Task<Entidades.Usuario> RecuperarUsuarioViaId(long id);
}
