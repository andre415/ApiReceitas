using Bogus;
using MeuLivroDeReceitas.Comunicacao.Requests;

namespace Utilitario.ParaTestes.Requests;

public class RequestAlterarSenhaBuilder
{
    public static RequestAlterarSenhaJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequestAlterarSenhaJson>()
            .RuleFor(c => c.SenhaAtual, f => f.Internet.Password(10))
            .RuleFor(c => c.NovaSenha, f => f.Internet.Password(tamanhoSenha));
    }
}
