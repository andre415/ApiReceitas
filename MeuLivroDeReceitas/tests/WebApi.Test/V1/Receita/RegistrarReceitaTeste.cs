using FluentAssertions;
using MeuLivroDeReceitas.Domain.Entidades;
using System.Net;
using System.Text.Json;
using Utilitario.ParaTestes.Requests;

namespace WebApi.Test.V1.Receita;
public class RegistrarReceitaTeste : ControllerBase
{
    private const string METODO = "receitas";
    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;
    public RegistrarReceitaTeste(MeuLivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RetornarUsuario();
        _senha = factory.RetornarSenha();
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequestReceitaJsonBuilder.Construir();


        var token = await Login(_usuario.Email, _senha);

        
        var resposta = await PostRequest(METODO, request,token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

       // responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }
}
