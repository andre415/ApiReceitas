using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepo.Repositorio;
public class ConexaoRepositorio: IConexaoWriteOnly, IConexaoReadOnly
{
    private readonly MeuLivroDeReceitasContext _context;

    public ConexaoRepositorio(MeuLivroDeReceitasContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteConexao(long idUsuarioA, long idUsuarioB)
    {
        return await _context.Conexao.AnyAsync(c => c.UsuarioId == idUsuarioA && c.ConectadoComUsuarioId == idUsuarioB);
    }

    public async Task<IList<Usuario>> RecuperarDoUsuario(long idUsuario)
    {
        return await _context.Conexao.AsNoTracking()
            .Include(c => c.ConectadoComUsuario)
            .Where(c => c.UsuarioId == idUsuario)
            .Select(c => c.ConectadoComUsuario)
            .ToListAsync();
    }

    public async Task Registrar(Conexao conexao)
    {
       await _context.Conexao.AddAsync(conexao);
    }
}
