using Bogus;
using MeuLivroDeReceitas.Comunicacao.Requests;

namespace Utilitario.ParaTestes.Requests;

public class RequestRegistarUsuarioBuilder
{
    public static RequestUsuarioJson Construir(int tamanhoSenha=10)
    {
        return new Faker<RequestUsuarioJson>()
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## 9 ####-####"));
    }
}
