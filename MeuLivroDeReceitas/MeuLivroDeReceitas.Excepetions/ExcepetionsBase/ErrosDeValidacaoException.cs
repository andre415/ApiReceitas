namespace MeuLivroDeReceitas.Excepetions.ExcepetionsBase;

public class ErrosDeValidacaoException: MeuLivroDeReceitasExcepetion
{
    public List<String> MensagensDeErro { get; set; }
    public ErrosDeValidacaoException (List<string> mesagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mesagensDeErro;
    }
}
