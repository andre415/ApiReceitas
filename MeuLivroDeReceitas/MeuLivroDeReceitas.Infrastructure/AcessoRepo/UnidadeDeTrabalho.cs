using MeuLivroDeReceitas.Domain.Repositorio;
namespace MeuLivroDeReceitas.Infrastructure.AcessoRepo;

public  sealed class UnidadeDeTrabalho : IDisposable, IunidadeDeTrabalho
{
    private readonly MeuLivroDeReceitasContext _context ;
    private bool _disposed;

    public UnidadeDeTrabalho(MeuLivroDeReceitasContext context)
    {
        _context = context;
    }

    public async Task Comit()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}
