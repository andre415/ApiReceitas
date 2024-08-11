using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using System.Security.AccessControl;
using Utilitario.ParaTestes.Encriptador;
using Utilitario.ParaTestes.Entidades;
using Utilitario.ParaTestes.Mapper;
using Utilitario.ParaTestes.Repositorios;
using Utilitario.ParaTestes.Token;

namespace UseCases.Test.FazerLogin;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha )= UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        var resposta = await useCase.Exucutar(new RequestLoginJson
        {
            Email = usuario.Email,
            Senha = senha
        });

        resposta.Should().NotBeNull();
        resposta.Nome.Should().Be(usuario.Nome);
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    } 
    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Exucutar(new RequestLoginJson
            {
                Email = usuario.Email,
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exeption => exeption.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));

    }
    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Exucutar(new RequestLoginJson
            {
                Email = "invalido@invalido.com",
                Senha = senha
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exeption => exeption.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));

    }
    [Fact]
    public async Task Validar_Erro_Login()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Exucutar(new RequestLoginJson
            {
                Email = "invalido@invalido.com",
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exeption => exeption.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));

    }
    private static LoginUseCase CriarUseCase( MeuLivroDeReceitas.Domain.Entidades.Usuario usuario )
    {
        var tokem = TokenControllerBuilder.Intancia();
        var encriptador = EncriptadorDeSenhaBuilder.Intancia();
        var repositorioReadOnly = UsuarioReadOnlyBuilder.Instance().RecuperarPorEmailSenha(usuario).Contruir();
        return new LoginUseCase(repositorioReadOnly ,encriptador, tokem);
    }
}
