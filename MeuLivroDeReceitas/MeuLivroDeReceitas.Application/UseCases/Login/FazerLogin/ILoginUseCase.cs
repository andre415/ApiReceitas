using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;

public interface ILoginUseCase
{
    Task<RespostaLoginJson> Exucutar(RequestLoginJson request);
}
