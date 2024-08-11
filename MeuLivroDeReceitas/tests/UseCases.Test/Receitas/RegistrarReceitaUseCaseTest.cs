using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;
using MeuLivroDeReceitas.Domain.Entidades;
using Utilitario.ParaTestes.Entidades;
using Utilitario.ParaTestes.Mapper;
using Utilitario.ParaTestes.Repositorios;
using Utilitario.ParaTestes.Requests;
using Utilitario.ParaTestes.UsuarioLogado;

namespace UseCases.Test.Receitas;
public class RegistrarReceitaUseCaseTest
{
    [Fact]
    public async Task Validar_Susesso()
    {
        var request = RequestReceitaJsonBuilder.Construir();
        var useCase = CriarUseCase();
        var resposta = await useCase.Executar(request);

        resposta.Should().NotBeNull();
        resposta.Titulo.Should().BeEquivalentTo(request.Titulo);
    }

    private RegistrarReceitasUseCase CriarUseCase()
    {
        var usuarioLogado = UsuarioLogadoBuilder.Instance().RecupearUsuario(UsuarioBuilder.Construir().usuario).Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instance().Contruir();
        var mapper = MapperBuilder.Instancia();
        var repositorioWriteOnly = ReceitaWriteonlyBuilder.Instance().Contruir();
        return new RegistrarReceitasUseCase(mapper, unidadeDeTrabalho, usuarioLogado, repositorioWriteOnly);
    }
}
