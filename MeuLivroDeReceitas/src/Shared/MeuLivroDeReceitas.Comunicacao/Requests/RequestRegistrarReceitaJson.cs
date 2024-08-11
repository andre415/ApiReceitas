using MeuLivroDeReceitas.Comunicacao.Enum;

namespace MeuLivroDeReceitas.Comunicacao.Requests;

public class RequestRegistrarReceitaJson
{
    public RequestRegistrarReceitaJson()
    {
        Ingredientes = new();
    }
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public int TempoDePreparo { get; set; }
    public string ModoDePreparo { get; set; }
    public List<RequestRegistrarIngredientesJson> Ingredientes { get; set; }
}
