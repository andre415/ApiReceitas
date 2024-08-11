using MeuLivroDeReceitas.Comunicacao.Requests;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;

public interface IAlterarSenhaUseCase
{
    Task Executar(RequestAlterarSenhaJson request);
}
