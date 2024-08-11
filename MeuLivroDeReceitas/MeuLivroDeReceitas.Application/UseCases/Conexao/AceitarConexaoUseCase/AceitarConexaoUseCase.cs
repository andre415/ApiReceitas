using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Domain.Repositorio.Codigo;
using MeuLivroDeReceitas.Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.AceitarConexaoUseCase;
public class AceitarConexaoUseCase : IAceitarConexaoUseCase
{
    private readonly IunidadeDeTrabalho _unidadeDeTrabalho;
    private readonly ICodigoWriteOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IConexaoWriteOnly _conexaoWriteOnlyRepositorio;

    public AceitarConexaoUseCase(ICodigoWriteOnlyRepositorio repositorio,
        IUsuarioLogado usuarioLogado, IunidadeDeTrabalho unidadeDeTrabalho, IConexaoWriteOnly conexaoWriteOnlyRepositorio)
    {
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _conexaoWriteOnlyRepositorio = conexaoWriteOnlyRepositorio;
    }

    public async Task<string> Executar(string idUsuarioParaSeConectar)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        await _repositorio.Deletar(usuarioLogado.Id);

        await _conexaoWriteOnlyRepositorio.Registrar(new Domain.Entidades.Conexao
        {
            UsuarioId = usuarioLogado.Id,
            ConectadoComUsuarioId = long.Parse(idUsuarioParaSeConectar)
        });

        await _conexaoWriteOnlyRepositorio.Registrar(new Domain.Entidades.Conexao
        {
            UsuarioId = long.Parse(idUsuarioParaSeConectar),
            ConectadoComUsuarioId = usuarioLogado.Id
        });

        await _unidadeDeTrabalho.Comit();

        return usuarioLogado.Id.ToString();
    }
}
