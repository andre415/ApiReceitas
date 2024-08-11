using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeuLivroDeReceitas.API.Filtros;

public class FiltroDasExceptions : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is MeuLivroDeReceitasExcepetion)
        {
            TratarMeuLivroDeReceitasExcepetion(context);
        }
        else
        {
            LancarErroDesconhecido(context);
        }
    }

    private void TratarMeuLivroDeReceitasExcepetion(ExceptionContext context)
    {
        if (context.Exception is ErrosDeValidacaoException)
        {
            TratarErrosDeValidacaoException(context);
        } else if(context.Exception is LoginInvalidoException)
        {
            TratarLoginException(context);

        }
    }

    private void TratarErrosDeValidacaoException(ExceptionContext context)
    {
        var errosDeValidacaoException = context.Exception as ErrosDeValidacaoException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new RespostaErroJson(errosDeValidacaoException.MensagensDeErro));
    }

    private void TratarLoginException(ExceptionContext context)
    {
        var loginInvalidoException = context.Exception as LoginInvalidoException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new RespostaErroJson(loginInvalidoException.Message));
    }
    private void LancarErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new RespostaErroJson(ResourceMensagensDeErro.ERRO_DESCONHECIDO));
    }

}
