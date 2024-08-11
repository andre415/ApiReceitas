using MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers
{
    public class LoginController : MeuLivroDeReceitasController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaLoginJson), StatusCodes.Status200OK)]
        public  async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,
            [FromBody] RequestLoginJson request)
        {
            var resposta = await useCase.Exucutar(request);
            return Ok(resposta);
        }
    }
}
