using Bogus;
using MeuLivroDeReceitas.Comunicacao.Enum;
using MeuLivroDeReceitas.Comunicacao.Requests;

namespace Utilitario.ParaTestes.Requests;
public class RequestReceitaJsonBuilder
{
    public static RequestRegistrarReceitaJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarReceitaJson>()
            .RuleFor(c => c.Titulo, f => f.Person.FullName)
            .RuleFor(c => c.Categoria, f => f.PickRandom<Categoria>())
            .RuleFor(c => c.ModoDePreparo , f => f.Lorem.Paragraph())
            .RuleFor(c => c.Ingredientes, f => ConstruirIngrediente(f))
            .RuleFor(c => c.TempoDePreparo, f => f.Random.Int(1, 10));
    }

    public static List<RequestRegistrarIngredientesJson> ConstruirIngrediente(Faker f)
    {
        List<RequestRegistrarIngredientesJson> ingrediente = new();

        ingrediente.Add(new RequestRegistrarIngredientesJson
        {
            Produto = f.Commerce.ProductName(),
            Quantidade = $"{f.Random.Double(1,10)}"
        });

        return ingrediente;
    }
}
