using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using Utilitario.ParaTestes.Encriptador;
using Utilitario.ParaTestes.Mapper;
using Utilitario.ParaTestes.Repositorios;
using Utilitario.ParaTestes.Requests;
using Utilitario.ParaTestes.Token;

namespace UseCases.Test.Usuario;

public class RegistrarUsuarioUseCaseTest 
{
    [Fact]
    public async Task Validar_Susesso()
    {
        var request = RequestRegistarUsuarioBuilder.Construir();
        var useCase = CriarUseCase();
        var resposta = await useCase.Executar(request);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();

    }

    [Fact]
    public async Task Validar_Erro_Email_Ja_Cadastrado()
    {
        var request = RequestRegistarUsuarioBuilder.Construir();
        var useCase = CriarUseCase(request.Email);

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exeption => exeption.MensagensDeErro.Count == 1 && exeption.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_JA_CADASTRADO));
    }
    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Email = string.Empty;
        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exeption => exeption.MensagensDeErro.Count == 1 && exeption.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));
    }

    private static RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var tokem = TokenControllerBuilder.Intancia();
        var encriptador = EncriptadorDeSenhaBuilder.Intancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instance().Contruir();
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWritecsOnlyBuilder.Instance().Contruir();
        var repositorioReadOnly = UsuarioReadOnlyBuilder.Instance().ExisteUsuarioComEmail(email).Contruir();
        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador
            , tokem, repositorioReadOnly);
    }
}