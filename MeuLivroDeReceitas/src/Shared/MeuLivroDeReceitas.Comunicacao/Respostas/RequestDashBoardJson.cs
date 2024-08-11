using MeuLivroDeReceitas.Comunicacao.Enum;

namespace MeuLivroDeReceitas.Comunicacao.Respostas;
public class RequestDashBoardJson
{
    public string TituloOuIngrediente { get; set; }
    public Categoria? Categoria { get; set; }
}