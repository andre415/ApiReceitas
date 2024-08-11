using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas.DeletarReceita;
public class DeletarReceitaUseCase : IDeletarReceitaUseCase
{
    
    private IunidadeDeTrabalho _unidadeDeTrabalho;
    private IUsuarioLogado _usuario;
    private IReceitasWriteOnlyRepositorio _repositorioWriteOnly;
    private IReceitaReadOnlyRepo _repositorioReadOnly;
    public DeletarReceitaUseCase(IunidadeDeTrabalho unidadeDeTrabalho,
        IUsuarioLogado usuario, IReceitaReadOnlyRepo repositorioReadOnly, 
        IReceitasWriteOnlyRepositorio repositorioWriteOnly)
    {
        _repositorioReadOnly = repositorioReadOnly;
        _repositorioWriteOnly = repositorioWriteOnly;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuario = usuario;
    }
    public async Task Executar(long id)
    {
        var  usuario = await _usuario.RecuperarUsuario();
        var receita = await _repositorioReadOnly.RecuperarReceitaPorId(id);

        Validar(usuario, receita);

        await _repositorioWriteOnly.Deletar(id);

        await _unidadeDeTrabalho.Comit();
    }

    private void Validar(Domain.Entidades.Usuario usuario, Receita receita)
    {
        if (receita == null || receita.UsuarioId != usuario.Id)
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA });
        }

    }
}
