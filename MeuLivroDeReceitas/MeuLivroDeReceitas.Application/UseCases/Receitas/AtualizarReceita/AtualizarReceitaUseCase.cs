using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using MeuLivroDeReceitas.Excepetions;
using Azure.Core;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas;
public class AtualizarReceitaUseCase : IAtualizarReceitaUseCase
{
    private IMapper _mapper;
    private IunidadeDeTrabalho _unidadeDeTrabalho;
    private IUsuarioLogado _usuario;
    private IReceitaUpadateOnlyRepositorio _repositorio;

    public AtualizarReceitaUseCase(IMapper mapper, IunidadeDeTrabalho unidadeDeTrabalho,
            IUsuarioLogado usuario, IReceitaUpadateOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuario = usuario;
    }
        public async Task Executar(long id, RequestRegistrarReceitaJson request)
    {
        var usuario = _usuario.RecuperarUsuario().Result;
        var receita = await _repositorio.RecuperarReceitaPorId(id);

        Validar(usuario, receita,request);

        _mapper.Map(request, receita);

        /* foreach(Ingredientes ingrediente in receita.ingredientes)
        {
            ingrediente.ReceitaId1 = receita.Id;
        }
        */
         _repositorio.Update(receita);

        await _unidadeDeTrabalho.Comit();
    }
    private void Validar(Domain.Entidades.Usuario usuario, Receita receita, RequestRegistrarReceitaJson request)
    {
        if (receita == null || receita.UsuarioId != usuario.Id)
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA });
        }

        var validator = new RegistrarReceitaValidator();
        var resultado = validator.Validate(request);

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }

    }
}
