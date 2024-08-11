using MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;
using MeuLivroDeReceitas.Application.UseCases.DashBoard;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers
{
    public class DashBoardController : MeuLivroDeReceitasController
    {

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> AlterarSenha(
            [FromServices] IDashBoardUseCase useCase,
            [FromBody] RequestDashBoardJson request)
        {
            var resposta = await useCase.Executar(request);

            if (resposta.Receitas.Any())
            {
                return Ok(resposta);
            }
            return NoContent();
        }
    }
}