using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Codigo;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepo.Repositorio;
public class CodigoRepositorio : ICodigoWriteOnlyRepositorio, ICodigoReadOnlyRepositorio
{
    private readonly MeuLivroDeReceitasContext _context;

    public CodigoRepositorio(MeuLivroDeReceitasContext context)
    {
        _context = context;
    }

    public async Task Deletar(long usuarioId)
    {
       var codigos = await _context.Codigos.Where(c => c.UsuarioId == usuarioId).ToListAsync();

        if (codigos.Any())
        {
            _context.Codigos.RemoveRange(codigos);
        }
    }

    public async Task<Codigos> RecuperarEntidadeCodigo(string codigo)
    {
        return await _context.Codigos.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Codigo == codigo);
    }

    public async Task Registrar(Codigos codigo)
    {
        var codigoNoBancoDeDados = await _context.Codigos.FirstOrDefaultAsync(c => c.UsuarioId == codigo.UsuarioId);
        if (codigoNoBancoDeDados is not null)
        {
            _context.Codigos.Update(codigo);
        }
        else
        {
            await _context.Codigos.AddAsync(codigo);
        }
    }
}
