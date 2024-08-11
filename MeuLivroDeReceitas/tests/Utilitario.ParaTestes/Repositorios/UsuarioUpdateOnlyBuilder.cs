using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;

public class UsuarioUpdateOnlyBuilder
{
    private static UsuarioUpdateOnlyBuilder _instance;
    private readonly Mock<IRepositorioUpdate> _repositorio;

    private UsuarioUpdateOnlyBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IRepositorioUpdate>();
        }
    }


    public UsuarioUpdateOnlyBuilder RecupearPorId(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        _repositorio.Setup(c => c.RecuperarUsuarioViaId(usuario.Id)).ReturnsAsync(usuario);
        return this;
    }
    public static UsuarioUpdateOnlyBuilder Instance()
    {
        _instance = new UsuarioUpdateOnlyBuilder();
        return _instance;
    }
   
    public IRepositorioUpdate Construir()
    {
        return _repositorio.Object;
    }
}
