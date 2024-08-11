using FluentAssertions;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using Utilitario.ParaTestes.Encriptador;
using Utilitario.ParaTestes.Entidades;
using Utilitario.ParaTestes.Repositorios;
using Utilitario.ParaTestes.Token;
using Utilitario.ParaTestes.UsuarioLogado;

namespace UseCases.Test.Usuario;

public class AlterarSenhaUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new RequestAlterarSenhaJson
            {
                SenhaAtual =  senha,
                 NovaSenha = "@Novasenha1234"
            });
        };

        await acao.Should().NotThrowAsync();
    }
    [Fact]
    public async Task Validar_Erro_NovaSenha_Em_Branco()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new RequestAlterarSenhaJson
            {
                SenhaAtual = senha,
                NovaSenha = ""
            });
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>().
            Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));
    }
    [Fact]
    public async Task Validar_Erro_NovaSenha_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new RequestAlterarSenhaJson
            {
                SenhaAtual = "invalida123",
                NovaSenha = "@Novasenha1234"
            });
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>().
            Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_ATUAL_INVALIDA));
    }
    [Fact]
    public async Task Validar_Erro_Senha_Atual_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new RequestAlterarSenhaJson
            {
                SenhaAtual = "13232",
                NovaSenha = "@Novasenha1234"
            });
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>().
            Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_ATUAL_INVALIDA));
    }

    private static AlterarSenhaUseCase CriarUseCase(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        var tokem = TokenControllerBuilder.Intancia();
        var encriptador = EncriptadorDeSenhaBuilder.Intancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instance().Contruir();
        var repositorio = UsuarioUpdateOnlyBuilder.Instance().RecupearPorId(usuario).Construir();
        var usuarioLogado = UsuarioLogadoBuilder.Instance().RecupearUsuario(usuario).Construir();

        return new AlterarSenhaUseCase(repositorio, usuarioLogado, unidadeDeTrabalho, encriptador);
    }
}
