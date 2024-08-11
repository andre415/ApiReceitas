using FluentAssertions;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Excepetions;
using System.Net;
using System.Text.Json;
using Utilitario.ParaTestes.Requests;

namespace WebApi.Test.V1.Login;

public class LoginTest : ControllerBase
{
    private const string METODO = "login";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;
    public LoginTest(MeuLivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RetornarUsuario();
        _senha = factory.RetornarSenha();
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = new RequestLoginJson
        {
            Email = _usuario.Email,
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().Be(_usuario.Nome);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();

    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        var request = new RequestLoginJson
        {
            Email = "invalido@inavalido.com",
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceMensagensDeErro.LOGIN_INVALIDO);

    }
    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        var request = new RequestLoginJson
        {
            Email = _usuario.Email,
            Senha = "invalido12345"
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceMensagensDeErro.LOGIN_INVALIDO);

    }

}
