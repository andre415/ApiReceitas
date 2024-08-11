using FluentAssertions;
using MeuLivroDeReceitas.Excepetions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text.Json;
using Utilitario.ParaTestes.Requests;
using WebApi.Test.V1;
using Xunit;

namespace WebApi.Test.Usuario;

public class RegistrarUsuarioTeste : ControllerBase
{
    private const string METODO = "usuario";
    public RegistrarUsuarioTeste(MeuLivroDeReceitasWebApplicationFactory<Program> factory): base(factory)
    {

    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequestRegistarUsuarioBuilder.Construir();

        var resposta = await PostRequest(METODO ,request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Validar_Erro_Nome_Em_Branco()
    {
        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Nome = "";

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO);
    }
}