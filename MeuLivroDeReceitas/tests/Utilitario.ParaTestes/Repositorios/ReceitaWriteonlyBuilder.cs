using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;
public class ReceitaWriteonlyBuilder
{
    private static ReceitaWriteonlyBuilder _instance;
    private readonly Mock<IReceitasWriteOnlyRepositorio> _repositorio;

    private ReceitaWriteonlyBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IReceitasWriteOnlyRepositorio>();
        }
    }

    public static ReceitaWriteonlyBuilder Instance()
    {
        _instance = new ReceitaWriteonlyBuilder();
        return _instance;
    }

    public IReceitasWriteOnlyRepositorio Contruir()
    {
        return _repositorio.Object;
    }
}
