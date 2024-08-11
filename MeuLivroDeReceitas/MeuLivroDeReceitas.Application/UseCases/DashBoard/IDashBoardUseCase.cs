using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.DashBoard;
public interface IDashBoardUseCase
{
    public Task<RespostaDashBoardJson> Executar(RequestDashBoardJson request);
}
