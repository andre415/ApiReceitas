using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;

public class UsuarioReadOnlyBuilder
{
    private static UsuarioReadOnlyBuilder _instance;
    private readonly Mock<IRepositorioUsuarioReadOnly> _repositorio;

    private UsuarioReadOnlyBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IRepositorioUsuarioReadOnly>();
        }
    }

    public static UsuarioReadOnlyBuilder Instance()
    {
        _instance = new UsuarioReadOnlyBuilder();
        return _instance;
    }

    public UsuarioReadOnlyBuilder ExisteUsuarioComEmail(string email)
    {
        if (!string.IsNullOrEmpty(email)){
            _repositorio.Setup(i => i.ExisteUsuarioComEmail(email)).ReturnsAsync(true);
        }
        return this;
    }

    public UsuarioReadOnlyBuilder RecuperarPorEmailSenha(Usuario usuario)
    {
        _repositorio.Setup(i => i.Login(usuario.Email, usuario.Senha)).ReturnsAsync(usuario);
        return this;
    }

    public IRepositorioUsuarioReadOnly Contruir()
    {
        return _repositorio.Object;
    }
}
