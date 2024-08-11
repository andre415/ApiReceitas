using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IRepositorioUsuarioReadOnly _repositorioUsuarioReadOnly;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenControler _tokenControler;
    public LoginUseCase(IRepositorioUsuarioReadOnly repositorioUsuarioReadOnly, EncriptadorDeSenha encriptadorDeSenha,
        TokenControler tokenControler)
    {
        _encriptadorDeSenha = encriptadorDeSenha;
        _repositorioUsuarioReadOnly = repositorioUsuarioReadOnly;
        _tokenControler = tokenControler;
    }
    public async Task<RespostaLoginJson> Exucutar(RequestLoginJson request)
    {
        var senhaCriptografada =  _encriptadorDeSenha.Criptografar(request.Senha);

        var usuario = await _repositorioUsuarioReadOnly.Login(request.Email, senhaCriptografada);

        if(usuario == null)
        {
            throw new LoginInvalidoException();
        }
        
        return new RespostaLoginJson
        {
            Nome = usuario.Nome,
            Token = _tokenControler.GerarToken(usuario.Email)

        };
    }
}
