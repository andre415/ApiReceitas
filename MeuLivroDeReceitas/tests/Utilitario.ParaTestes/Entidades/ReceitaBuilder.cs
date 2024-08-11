using Bogus;
using MeuLivroDeReceitas.Comunicacao.Enum;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Entidades;

namespace Utilitario.ParaTestes.Entidades;
public class ReceitaBuilder
{
    public static Receita ConstruirReceita(long idUsuario)
    {
        return new Faker<Receita>()
            .RuleFor(c => c.Titulo, f => f.Person.FullName)
            .RuleFor(c => c.Id, _ => idUsuario)
            .RuleFor(c => c.UsuarioId, _ => idUsuario)
            .RuleFor(c => c.Titulo, f => f.Person.FullName)
            .RuleFor(c => c.TempoDePreparo, f => f.Random.Int(1,10))
            .RuleFor(c => c.ModoDePreparo, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Categoria, f => f.PickRandom<MeuLivroDeReceitas.Domain.Enum.Categoria>())
            .RuleFor(c => c.ingredientes, f => ConstruirIngrediente(f,idUsuario));
    }

    public static List<Ingredientes> ConstruirIngrediente(Faker f, long idReceita)
    {
        List<Ingredientes> ingrediente = new();

        ingrediente.Add(new Ingredientes
        {
            Id = idReceita,
            Produto = f.Commerce.ProductName(),
            Quantidade = $"{f.Random.Double(1, 10)}"
        });

        return ingrediente;
    }
}
