using MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;
using MeuLivroDeReceitas.Application.UseCases.Receitas;
using MeuLivroDeReceitas.Application.UseCases.Receitas.DeletarReceita;
using MeuLivroDeReceitas.Application.UseCases.Receitas.RecuperarPorId;
using MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class ReceitasController : MeuLivroDeReceitasController
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaReceitaJson),StatusCodes.Status201Created)]
    public async Task <IActionResult> Registrar(
        [FromServices] IRegistrarReceitasUseCase useCase, 
        [FromBody] RequestRegistrarReceitaJson request)
    {

        var resposta = await useCase.Executar(request);
        return Created(string.Empty,resposta);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(RespostaReceitaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperarPorId(

        [FromServices] IRecuperarReceitaPorIdUseCase useCase,
        [FromRoute] Int64 id)
    {
        var resposta = await useCase.Executar(id);
        return Ok(resposta);
    }


    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(RespostaReceitaJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Atualizar(

        [FromServices] IAtualizarReceitaUseCase useCase,
        [FromRoute] Int64 id,
        [FromBody] RequestRegistrarReceitaJson request)
    {
        await useCase.Executar(id,request);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar(

        [FromServices] IDeletarReceitaUseCase useCase,
        [FromRoute] Int64 id)
    {
        await useCase.Executar(id);
        return NoContent();
    }
}
