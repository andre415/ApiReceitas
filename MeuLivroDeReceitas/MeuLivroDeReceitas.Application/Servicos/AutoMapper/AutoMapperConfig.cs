using AutoMapper;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Application.Servicos.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        RequestParaEntidade();
        EntidadeParaResposta();

    }

    private void RequestParaEntidade()
    {
        CreateMap<RequestUsuarioJson, Domain.Entidades.Usuario>().
           ForMember(destino => destino.Senha, cofig => cofig.Ignore());

        CreateMap<RequestRegistrarReceitaJson,Receita >();

        CreateMap<RequestRegistrarIngredientesJson, Ingredientes>();
    }

    private void EntidadeParaResposta()
    {
        CreateMap<Domain.Entidades.Usuario, RespostaPerfilJson>();
        CreateMap<Domain.Entidades.Usuario, RespostaUsuarioConectadoJson>();
        CreateMap<Receita,RespostaReceitaJson>();
        CreateMap<Ingredientes,RespostaIngredienteJson>();
        CreateMap<Receita, RespostaReceitaDashBoardJson>()
        .ForMember(destino => destino.QuantidadeDeIngredientes, config => config.MapFrom(origem => origem.ingredientes.Count()));
    }
}
