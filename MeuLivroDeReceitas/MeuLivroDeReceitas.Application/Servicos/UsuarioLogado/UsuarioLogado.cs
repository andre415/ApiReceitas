using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Microsoft.AspNetCore.Http;

namespace MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;

public class UsuarioLogado : IUsuarioLogado
{
    private IHttpContextAccessor _httpContext;
    private TokenControler _tokenControler;
    private IRepositorioUsuarioReadOnly _repositorio;
    public UsuarioLogado(IHttpContextAccessor httpContext, TokenControler tokenControler, IRepositorioUsuarioReadOnly repositorio)
    {
        _repositorio = repositorio;
        _httpContext= httpContext;
        _tokenControler= tokenControler;
    }
    public async Task<Usuario> RecuperarUsuario()
    {
        var authorization = _httpContext.HttpContext.Request.Headers["authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var usuario = await _repositorio.RecuperarPorEmail(_tokenControler.RecuperarEmail(token));

        return usuario;
    }
}

