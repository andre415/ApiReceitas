using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;

public class UsuarioLogadoHandler : AuthorizationHandler<UsuarioLogadoRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private TokenControler _tokenControler;
    private IRepositorioUsuarioReadOnly _repositorio;

    public UsuarioLogadoHandler(IHttpContextAccessor contextAccessor,
        TokenControler tokenControler,
        IRepositorioUsuarioReadOnly repositorio)
    {
        _repositorio = repositorio;
        _tokenControler = tokenControler;
        _contextAccessor = contextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UsuarioLogadoRequirement requirement)
    {
        try
        {
            var authorization = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorization))
            {
                context.Fail();
                return;
            }

            var token = authorization["Bearer".Length..].Trim();

            var usuario = await _repositorio.RecuperarPorEmail(_tokenControler.RecuperarEmail(token));

            if (usuario is null)
            {
                context.Fail();
                return;
            }
               
            context.Succeed(requirement);
        }
        catch
        {
            context.Fail();
            return;
        }
       
    }
  
}
