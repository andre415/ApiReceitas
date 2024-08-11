using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Domain.Repositorio.Usuario;

public interface IRepositorioUsuarioWriteOnly
{
    Task Adicionar(Entidades.Usuario usuario);
}
