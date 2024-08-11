using FluentAssertions;
using MeuLivroDeReceitas.Comunicacao.Requests;
using System.Net;
using Utilitario.ParaTestes.Requests;

namespace WebApi.Test.V1.Usuario
{
    public class AlterarSenhaTeste : ControllerBase
    {
        private const string METODO = "usuario/alterar-senha";

        private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
        private string _senha;
        public AlterarSenhaTeste(MeuLivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
        {
            _usuario = factory.RetornarUsuario();
            _senha = factory.RetornarSenha();
        }

        [Fact]
        public async Task Validar_Sucesso() {
            var request = RequestAlterarSenhaBuilder.Construir();

            var token = await Login(_usuario.Email, _senha);
            request.SenhaAtual = _senha;

            var resposta = await PutRequest(METODO, request);
            
           
            resposta.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
