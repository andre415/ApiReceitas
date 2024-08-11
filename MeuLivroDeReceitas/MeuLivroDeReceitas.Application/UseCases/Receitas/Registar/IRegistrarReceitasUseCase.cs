using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas.Registar
{
    public interface IRegistrarReceitasUseCase
    {
        Task<RespostaReceitaJson> Executar(RequestRegistrarReceitaJson request); 
    }
}
