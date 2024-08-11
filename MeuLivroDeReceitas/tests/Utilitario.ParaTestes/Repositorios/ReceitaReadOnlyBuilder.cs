using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;
public class ReceitaReadOnlyBuilder
{
    private static ReceitaReadOnlyBuilder _instance;
    private readonly Mock<IReceitaReadOnlyRepo> _repositorio;

    private ReceitaReadOnlyBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IReceitaReadOnlyRepo>();
        }
    }

    public static ReceitaReadOnlyBuilder Instance()
    {
        _instance = new ReceitaReadOnlyBuilder();
        return _instance;
    }

    public IReceitaReadOnlyRepo Contruir()
    {
        return _repositorio.Object;
    }
}
