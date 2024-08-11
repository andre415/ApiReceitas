namespace MeuLivroDeReceitas.Comunicacao.Respostas;
public class RespostaIngredienteJson
{
    public long Id { get; set; }
    public string Produto { get; set; }
    public string Quantidade { get; set; }

    public long ReceitaId1;
}
