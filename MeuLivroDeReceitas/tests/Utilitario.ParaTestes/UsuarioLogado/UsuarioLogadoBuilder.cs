using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;
using Utilitario.ParaTestes.Repositorios;

namespace Utilitario.ParaTestes.UsuarioLogado;

public class UsuarioLogadoBuilder
{
    private static UsuarioLogadoBuilder _instance;
    private readonly Mock<IUsuarioLogado> _repositorio;

    private UsuarioLogadoBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IUsuarioLogado>();
        }
    }


    public UsuarioLogadoBuilder RecupearUsuario(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        _repositorio.Setup(c => c.RecuperarUsuario()).ReturnsAsync(usuario);
        return this;
    }
    public static UsuarioLogadoBuilder Instance()
    {
        _instance = new UsuarioLogadoBuilder();
        return _instance;
    }
    public IUsuarioLogado Construir()
    {
        return _repositorio.Object;
    }
}
