using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Codigo;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao;
public class GerarQrCodeUseCase : IGerarQrCodeUseCase
{
    private readonly ICodigoWriteOnlyRepositorio _codigoWriteOnlyRepositorio;
    private readonly IunidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IUsuarioLogado _usuarioLogado;

    public GerarQrCodeUseCase(ICodigoWriteOnlyRepositorio codigoWriteOnlyRepositorio, IunidadeDeTrabalho unidadeDeTrabalho, IUsuarioLogado usuarioLogado)
    {
        _codigoWriteOnlyRepositorio = codigoWriteOnlyRepositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<(string QRCode, string IdUsuario)> Executar()
    {
        var usuario = await _usuarioLogado.RecuperarUsuario();
        var codigo = new Codigos
        {
            Codigo = Guid.NewGuid().ToString(),
            UsuarioId = usuario.Id,

        };

        await _codigoWriteOnlyRepositorio.Registrar(codigo);
        await _unidadeDeTrabalho.Comit();

        return (codigo.Codigo, usuario.Id.ToString());
       
    }
}
