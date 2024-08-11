using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.Remover;
public class RemoverConexaoUseCase : IRemoverConexaoUseCase
{
    private readonly IConexaoWriteOnly _conexaoWriteOnlyRepositorio;
    private readonly IConexaoReadOnly _conexaoReadOnlyRepositorio;
    private readonly IUsuarioLogado _usuarioLogado;


    public RemoverConexaoUseCase(IConexaoWriteOnly conexaoWriteOnlyRepositorio,
        IUsuarioLogado usuarioLogado,
        IConexaoReadOnly conexaoReadOnlyRepositorio)
    {
        _conexaoWriteOnlyRepositorio = conexaoWriteOnlyRepositorio;
        _conexaoReadOnlyRepositorio = conexaoReadOnlyRepositorio;
        _usuarioLogado = usuarioLogado;
    }
    public Task Executar(long idUsurioConectadoParaRemover)
    {
        throw new NotImplementedException();
    }//
}
