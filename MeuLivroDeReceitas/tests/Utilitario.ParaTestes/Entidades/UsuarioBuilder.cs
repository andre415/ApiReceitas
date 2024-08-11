using Bogus;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Entidades;
using Utilitario.ParaTestes.Encriptador;

namespace Utilitario.ParaTestes.Entidades;

public class UsuarioBuilder
{
    public static (Usuario usuario, string senha) Construir()
    {
        string senha = string.Empty;

        var usuarioGerado=  new Faker<Usuario>()
            .RuleFor(c => c.Id, _ => 1)
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f =>
            {
                senha = f.Internet.Password();
                return EncriptadorDeSenhaBuilder.Intancia().Criptografar(senha);
            })
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## 9 ####-####"));
        return (usuarioGerado, senha);
    }
}
