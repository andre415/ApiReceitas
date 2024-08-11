namespace MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
public class LoginInvalidoException : MeuLivroDeReceitasExcepetion
{
    public LoginInvalidoException(): base(ResourceMensagensDeErro.LOGIN_INVALIDO){}
}
