using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Codigo;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.QRCodeLidoUseCase;
public class QRCodeLidoUseCase : IQRCodeLidoUseCase
{
    private readonly IConexaoReadOnly _repositorioConexao;
    private readonly ICodigoReadOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;

    public QRCodeLidoUseCase(ICodigoReadOnlyRepositorio repositorio,
        IUsuarioLogado usuarioLogado, IConexaoReadOnly repositorioConexao)
    {
        _repositorioConexao = repositorioConexao;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<(RespostaConexaoUsuarioJson UsuarioConectado,string idUsuarioQueGerouQRCode)> 
        Executar(string codigoConexao)
    {

        var usuario = await _usuarioLogado.RecuperarUsuario();
        var codigo = await _repositorio.RecuperarEntidadeCodigo(codigoConexao);
        await Validar(codigo,usuario);

        var usuarioConectado = new RespostaConexaoUsuarioJson()
        {
            Id = usuario.Id.ToString(),
            Nome = usuario.Nome
        };
        return (usuarioConectado,codigo.UsuarioId.ToString());
    }

    private async Task Validar(Codigos codigo,Domain.Entidades.Usuario usuario)
    {
        if( codigo is null)
        {
            throw new MeuLivroDeReceitasExcepetion("");
        }

        if (codigo.UsuarioId == usuario.Id)
        {
            throw new MeuLivroDeReceitasExcepetion("");
        }

        var existeConexao = await _repositorioConexao.ExisteConexao(codigo.UsuarioId, usuario.Id);

        if (existeConexao)
        {
            throw new MeuLivroDeReceitasExcepetion("");
        }
    }
}
