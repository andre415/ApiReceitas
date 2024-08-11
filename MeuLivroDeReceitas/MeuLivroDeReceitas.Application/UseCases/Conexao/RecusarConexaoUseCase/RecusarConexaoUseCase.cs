using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Codigo;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.RecusarConexaoUseCase;
public class RecusarConexaoUseCase : IRecusarConexaoUseCase
{
    private readonly IunidadeDeTrabalho _unidadeDeTrabalho;
    private readonly ICodigoWriteOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;

    public RecusarConexaoUseCase(ICodigoWriteOnlyRepositorio repositorio,
        IUsuarioLogado usuarioLogado, IunidadeDeTrabalho unidadeDeTrabalho)
    {
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<string> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        
        await _repositorio.Deletar(usuarioLogado.Id);

        await _unidadeDeTrabalho.Comit();

        return usuarioLogado.Id.ToString();
    }
}
