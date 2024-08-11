using MeuLivroDeReceitas.Comunicacao.Enum;
using MeuLivroDeReceitas.Comunicacao.Requests;

namespace MeuLivroDeReceitas.Comunicacao.Respostas;
public class RespostaReceitaJson
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoDePreparo { get; set; }
    public int TempoDePreparo { get; set; }
    public List<RespostaIngredienteJson> Ingredientes { get; set; }
}
