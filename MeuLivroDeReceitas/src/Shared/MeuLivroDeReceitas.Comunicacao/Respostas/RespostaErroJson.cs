namespace MeuLivroDeReceitas.Comunicacao.Respostas;

public class RespostaErroJson
{
    public List<String> Mensagens { get; set; }

    public RespostaErroJson(string mensagem)
    {
        Mensagens = new List<String>()
        {
            mensagem
        };
    }
    public RespostaErroJson(List<string>mensagens)
    {
        Mensagens = mensagens;
        
    }
}
