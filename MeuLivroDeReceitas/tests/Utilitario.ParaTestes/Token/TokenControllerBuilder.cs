using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;

namespace Utilitario.ParaTestes.Token;

public class TokenControllerBuilder
{
    public static TokenControler Intancia()
    {
        return new TokenControler(1000, "YF0mMjRYKMKjLDZsUV5qLFIoOU1ULkZSN2cnZD9RRXQ2XSM5cXFdWTF1XTt8LjpCVDtZ");

    }
}
