using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;
public class ReceitaUpadateOnlyBuilder
{
    private static ReceitaUpadateOnlyBuilder _instance;
    private readonly Mock<IReceitaUpadateOnlyRepositorio> _repositorio;

    private ReceitaUpadateOnlyBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IReceitaUpadateOnlyRepositorio>();
        }
    }

    public static ReceitaUpadateOnlyBuilder Instance()
    {
        _instance = new ReceitaUpadateOnlyBuilder();
        return _instance;
    }

    public IReceitaUpadateOnlyRepositorio Contruir()
    {
        return _repositorio.Object;
    }
}
