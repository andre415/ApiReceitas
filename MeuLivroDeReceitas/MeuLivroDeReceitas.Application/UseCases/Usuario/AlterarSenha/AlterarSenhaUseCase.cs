using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;

public class AlterarSenhaUseCase : IAlterarSenhaUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IRepositorioUpdate _repositorio;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly IunidadeDeTrabalho _unidadeDeTrabalho;
    public AlterarSenhaUseCase(IRepositorioUpdate repositorio, IUsuarioLogado usuarioLogado, 
        IunidadeDeTrabalho unidadeDeTrabalho ,EncriptadorDeSenha encriptadorDeSenha)
    {
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado; 
    }

    public  async Task Executar(RequestAlterarSenhaJson request)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        Domain.Entidades.Usuario usuario = _repositorio.RecuperarUsuarioViaId(usuarioLogado.Id).Result;

        Validar(request,usuario);

        usuario.Senha = _encriptadorDeSenha.Criptografar(request.NovaSenha);

        _repositorio.Upadate(usuario);
        await _unidadeDeTrabalho.Comit();
    }

    private void Validar(RequestAlterarSenhaJson request, Domain.Entidades.Usuario usuario)
    {
       var resultado= new SenhaValidator().Validate(request.NovaSenha);

        var senhaAtualCriptografada = _encriptadorDeSenha.Criptografar(request.SenhaAtual);

        if (!usuario.Senha.Equals(senhaAtualCriptografada))
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual"
                , ResourceMensagensDeErro.SENHA_ATUAL_INVALIDA));
        } 
        if (!resultado.IsValid)
        {
            var mensagens = resultado.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagens);
        }
    }
}
