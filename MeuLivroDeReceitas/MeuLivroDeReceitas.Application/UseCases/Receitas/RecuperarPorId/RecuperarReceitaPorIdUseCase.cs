using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using MeuLivroDeReceitas.Infrastructure.AcessoRepo.Repositorio;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas.RecuperarPorId;
public class RecuperarReceitaPorIdUseCase : IRecuperarReceitaPorIdUseCase
{
    private readonly IReceitaReadOnlyRepo _repositorio;
    private readonly IConexaoReadOnly _conexaoRepositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public RecuperarReceitaPorIdUseCase(IReceitaReadOnlyRepo repositorio,
        IUsuarioLogado usuarioLogado,
        IMapper mapper,
        IConexaoReadOnly conexaoRepositorio)
    {
        _conexaoRepositorio = conexaoRepositorio;
        _mapper = mapper;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
    }
    public async Task<RespostaReceitaJson> Executar(long Id)
    {
        var usuario = _usuarioLogado.RecuperarUsuario().Result;
        var receita = await _repositorio.RecuperarReceitaPorId(Id);

        await Validar(usuario, receita);
        
        var respota = _mapper.Map<RespostaReceitaJson>(receita);
        return respota;

    }

    private async Task Validar(Domain.Entidades.Usuario usuario, Receita receita)
    {
        var usuariosConectados = await _conexaoRepositorio.RecuperarDoUsuario(usuario.Id);

        if (receita is null || (receita.UsuarioId != usuario.Id && !usuariosConectados.Any(c => c.Id == receita.UsuarioId)))
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA});
        }
        
    }
}
