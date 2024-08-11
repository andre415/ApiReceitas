using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Enum;
using MeuLivroDeReceitas.Domain.Extensions;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.Application.UseCases.DashBoard;
public class DashBoardUseCase : IDashBoardUseCase
{
    private readonly IReceitaReadOnlyRepo _repositorio;
    private readonly IConexaoReadOnly _repositorioConexao;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public DashBoardUseCase(IReceitaReadOnlyRepo repositorio,
        IUsuarioLogado usuarioLogado,
        IMapper mapper,
        IConexaoReadOnly repositorioConexao)
    {
        _repositorioConexao = repositorioConexao;
        _mapper = mapper;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
    }
    public async Task<RespostaDashBoardJson> Executar(RequestDashBoardJson request)
    {
        var usuario = await _usuarioLogado.RecuperarUsuario();

        var receitas = _repositorio.RecuperarTodasDoUsuario(usuario.Id).Result;

        receitas = Filtrar(request, receitas);

        var conexoes = await _repositorioConexao.RecuperarDoUsuario(usuario.Id);

        var usuariosConectados = conexoes.Select(c => c.Id).ToList();
        var receitasUsuariosConectados = await _repositorio.RecuperarTodasDosUsuarios(usuariosConectados);

        receitas = receitas.Concat(receitasUsuariosConectados).ToList();

        return new RespostaDashBoardJson
        {
            Receitas = _mapper.Map<List<RespostaReceitaDashBoardJson>>(receitas)
        };
    }

    private static IList<Receita> Filtrar(RequestDashBoardJson request, IList<Receita> receitas)
    {
        if (request.Categoria.HasValue)
        {
            receitas = receitas.Where(x => x.Categoria == (Categoria)request.Categoria.Value).ToList();
        }

        if (!string.IsNullOrWhiteSpace(request.TituloOuIngrediente))
        {
           receitas = receitas.Where(r => r.Titulo.CompararSemAcentosOuLetrasMaiusculas(request.TituloOuIngrediente) ||
           r.ingredientes.Any(ingrediente => ingrediente.Produto.CompararSemAcentosOuLetrasMaiusculas(request.TituloOuIngrediente))).ToList();
        }

        return receitas.OrderBy(c => c.Titulo).ToList();
    }
}
