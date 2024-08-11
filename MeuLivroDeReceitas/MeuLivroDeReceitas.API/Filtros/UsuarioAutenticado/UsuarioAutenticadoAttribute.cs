using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Excepetions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;

public class UsuarioAutenticadoAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private TokenControler _tokenControler;
    private IRepositorioUsuarioReadOnly _repositorio;

    public UsuarioAutenticadoAttribute(TokenControler tokenControler, IRepositorioUsuarioReadOnly repositorio)
    {
        _repositorio = repositorio;
        _tokenControler = tokenControler;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenNaRequisicao(context);
            var email = _tokenControler.RecuperarEmail(token);

            var usuario = _repositorio.RecuperarPorEmail(email);

            if (usuario is null)
            {
                throw new Exception();
            }
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch
        {
            UsuarioSemPermissao(context);
        }
    }

    private string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["authorization"].ToString();

        if (string.IsNullOrEmpty(authorization))
        {
            throw new Exception();
        }
        return authorization["Bearer".Length..].Trim();
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO));
    }

    private void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_SEM_PERMISSAO));
    }
}
