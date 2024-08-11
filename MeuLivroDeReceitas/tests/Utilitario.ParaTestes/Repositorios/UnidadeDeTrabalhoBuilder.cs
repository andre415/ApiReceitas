using MeuLivroDeReceitas.Domain.Repositorio;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;

public class UnidadeDeTrabalhoBuilder
{
    private static UnidadeDeTrabalhoBuilder _instance;
    private readonly Mock<IunidadeDeTrabalho> _repositorio;

    private UnidadeDeTrabalhoBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IunidadeDeTrabalho>();
        }
    }

    public static UnidadeDeTrabalhoBuilder Instance()
    {
        _instance = new UnidadeDeTrabalhoBuilder();
        return _instance;
    }

    public IunidadeDeTrabalho Contruir()
    {
        return _repositorio.Object;
    }
}
