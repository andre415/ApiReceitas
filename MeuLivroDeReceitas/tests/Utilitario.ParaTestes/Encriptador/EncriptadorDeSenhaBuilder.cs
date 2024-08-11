using MeuLivroDeReceitas.Application.Servicos.Criptografia;

namespace Utilitario.ParaTestes.Encriptador;

public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Intancia()
    {
        return new EncriptadorDeSenha("ABCD123");
        
    }
}
