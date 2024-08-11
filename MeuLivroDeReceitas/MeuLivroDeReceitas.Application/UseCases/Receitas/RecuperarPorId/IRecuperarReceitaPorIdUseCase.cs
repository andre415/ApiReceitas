using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas.RecuperarPorId;
public interface IRecuperarReceitaPorIdUseCase
{
    Task<RespostaReceitaJson> Executar(long Id);
}
