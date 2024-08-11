using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using System.ComponentModel.DataAnnotations;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;

public class RegistrarReceitasUseCase : IRegistrarReceitasUseCase
{
    private IMapper _mapper;
    private IunidadeDeTrabalho _unidadeDeTrabalho;
    private IUsuarioLogado _usuario;
    private IReceitasWriteOnlyRepositorio _repositorio;
    public RegistrarReceitasUseCase(IMapper mapper, IunidadeDeTrabalho unidadeDeTrabalho ,
        IUsuarioLogado usuario , IReceitasWriteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuario = usuario;
    }
    public async Task<RespostaReceitaJson> Executar(RequestRegistrarReceitaJson request)
    {
        Validar(request);
        var usuarioLogado = await _usuario.RecuperarUsuario();

        var receita = _mapper.Map<Domain.Entidades.Receita>(request);
        receita.UsuarioId = usuarioLogado.Id;

        await _repositorio.Registrar(receita);
        await _unidadeDeTrabalho.Comit();

        return _mapper.Map<RespostaReceitaJson>(receita);
    }

    private void Validar(RequestRegistrarReceitaJson request)
    {
        
    }
}
