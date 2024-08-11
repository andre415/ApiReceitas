using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;

public interface IRegistrarUsuarioUseCase
{
    public  Task<RespostaUsuarioRegistradoJson> Executar(RequestUsuarioJson request);
}
