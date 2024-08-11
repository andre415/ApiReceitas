using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepo.Repositorio;

public class UsuarioRepositorio : IRepositorioUsuarioWriteOnly, IRepositorioUsuarioReadOnly, IRepositorioUpdate
{
    private MeuLivroDeReceitasContext _context;
    public UsuarioRepositorio(MeuLivroDeReceitasContext context)
    {
        _context = context;
    }
    public async Task Adicionar(Usuario usuario)
    {
        await _context.Usuario.AddAsync(usuario);
    }

    public async Task<bool> ExisteUsuarioComEmail(string email)
    {
        return await _context.Usuario.AnyAsync(c => c.Email.Equals(email));
    }

    public async Task<Usuario> Login(string email, string senha)
    {
        return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Senha.Equals(senha));
    }

    public async Task<Usuario> RecuperarPorEmail(string email)
    {
        return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email));
    }

    public async Task<Usuario> RecuperarUsuarioViaId(long id)
    {
        return await _context.Usuario.FirstOrDefaultAsync(c => c.Id == id);
    }

    public  void Upadate(Usuario usuario)
    {
        _context.Usuario.Update(usuario);
    }
}
