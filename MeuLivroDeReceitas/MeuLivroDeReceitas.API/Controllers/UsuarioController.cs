using MeuLivroDeReceitas.API.Filtros.UsuarioAutenticado;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers
{
    public class UsuarioController : MeuLivroDeReceitasController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarUsuario(
            [FromServices] IRegistrarUsuarioUseCase useCase,
            [FromBody] RequestUsuarioJson request)
        {
            var resultado = await useCase.Executar(request);
            return Created(string.Empty, resultado);
        }

        [HttpGet]
        [ProducesResponseType(typeof(RespostaPerfilJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> RecuperarPerfil(
           [FromServices] IRecuperarPerfilUseCase useCase)
        {
            var resultado = await useCase.Executar();
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar-senha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> AlterarSenha(
            [FromServices] IAlterarSenhaUseCase useCase,
            [FromBody] RequestAlterarSenhaJson  request)
        {
            await useCase.Executar(request);
            return NoContent();
        }
    }
}