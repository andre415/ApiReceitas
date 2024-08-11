using MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;
using MeuLivroDeReceitas.Application.UseCases.Conexao.RecuperarTodasAsConexoesUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.Remover;
using MeuLivroDeReceitas.Application.UseCases.DashBoard;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers
{
    public class ConexoesController : MeuLivroDeReceitasController
    {

        [HttpGet]
        [ProducesResponseType(typeof(IList<RespostaUsuarioConectadoJson>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> RecuperarConexoes(
            [FromServices] IRecuperarTodasAsConexoesUseCase useCase)
        {
           var resposta = await useCase.Executar();

            if (resposta.Any())
            {
                return Ok(resposta);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> RemoverConexao(
            [FromServices] IRemoverConexaoUseCase useCase,
            [FromRoute] Int64 id)
        {
            await useCase.Executar(id);
            return NoContent();
        }
    }
}