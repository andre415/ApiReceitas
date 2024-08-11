using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepo.Repositorio;

public class ReceitaRepositorio : IReceitasWriteOnlyRepositorio, IReceitaReadOnlyRepo, IReceitaUpadateOnlyRepositorio
{
    private readonly MeuLivroDeReceitasContext _context;

    public ReceitaRepositorio(MeuLivroDeReceitasContext context)
    {
        _context = context;
    }

    async Task<Receita> IReceitaUpadateOnlyRepositorio.RecuperarReceitaPorId(long ReceitaId1)
    {

        return await _context.Receitas
            .Include(r => r.ingredientes)
            .FirstOrDefaultAsync(r => r.Id == ReceitaId1);
    }

     async Task<Receita> IReceitaReadOnlyRepo.RecuperarReceitaPorId(long ReceitaId1)
    {
        return await _context.Receitas.
            AsNoTracking().
            Include(r => r.ingredientes)
            .FirstOrDefaultAsync(r => r.Id == ReceitaId1);
    }

    public async Task <IList<Receita>> RecuperarTodasDoUsuario(long usuarioId)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.ingredientes)
            .Where(r => r.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<IList<Receita>> RecuperarTodasDosUsuarios(List<long> usuariosIds)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.ingredientes)
            .Where(r => usuariosIds.Contains(r.UsuarioId )).ToListAsync();
    }

    public async Task Registrar(Receita receita )
    {
      await _context.Receitas.AddAsync(receita);
    }

    public void Update(Receita receita)
    {
        _context.Receitas.Update(receita);
       
    }

    public async Task Deletar(long id)
    {
        var receita = _context.Receitas.FirstOrDefault(r => r.Id == id);
        _context.Receitas.Remove(receita);
    }

    public async Task<int> QuantidadesDeReceitas(long idUsuario)
    {
        return await _context.Receitas.CountAsync(r => r.UsuarioId == idUsuario); 
            
    }
}
