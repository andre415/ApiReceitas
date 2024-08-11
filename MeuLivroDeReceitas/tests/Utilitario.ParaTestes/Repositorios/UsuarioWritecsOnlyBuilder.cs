using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Moq;

namespace Utilitario.ParaTestes.Repositorios;

public class UsuarioWritecsOnlyBuilder
{
    private static UsuarioWritecsOnlyBuilder _instance;
    private readonly Mock<IRepositorioUsuarioWriteOnly> _repositorio;

    private UsuarioWritecsOnlyBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IRepositorioUsuarioWriteOnly>();
        }
    }

    public static UsuarioWritecsOnlyBuilder Instance()
    {
        _instance = new UsuarioWritecsOnlyBuilder();
        return _instance;
    }

    public IRepositorioUsuarioWriteOnly Contruir() 
    {    
        return _repositorio.Object; 
    }
}
