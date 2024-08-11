using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.RecuperarTodasAsConexoesUseCase;
public class RecuperarTodasAsConexoesUseCase : IRecuperarTodasAsConexoesUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaReadOnlyRepo _receitaReadOnlyRepositorio;
    private readonly IConexaoReadOnly _repositorio;
    private readonly IMapper _mapper;

    public RecuperarTodasAsConexoesUseCase(IUsuarioLogado usuarioLogado,
        IConexaoReadOnly repositorio,
        IMapper mapper,
        IReceitaReadOnlyRepo receitaReadOnlyRepo)
    {
        _receitaReadOnlyRepositorio = receitaReadOnlyRepo;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<IList<RespostaUsuarioConectadoJson>> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var conexoes = await _repositorio.RecuperarDoUsuario(usuarioLogado.Id);

        var tarefas = conexoes.Select(async usuario =>
        {
            var quantidadesDeReceitas = await _receitaReadOnlyRepositorio.QuantidadesDeReceitas(usuario.Id);

            var usuarioJson = _mapper.Map<RespostaUsuarioConectadoJson>(usuario);

            usuarioJson.QuantidadeDeReceitas = quantidadesDeReceitas;
            return usuarioJson;
        });

        return await Task.WhenAll(tarefas);
    }
}
