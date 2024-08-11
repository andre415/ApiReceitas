using MeuLivroDeReceitas.Comunicacao.Requests;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas;
public interface IAtualizarReceitaUseCase
{
     Task Executar(long id, RequestRegistrarReceitaJson request);
}
