using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IMapper _mapper;
    private readonly IRepositorioUsuarioReadOnly _repositorioUsuarioReadOnly;
    private readonly IRepositorioUsuarioWriteOnly _repositorio;
    private readonly IunidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenControler _tokenControler;
    public RegistrarUsuarioUseCase(IRepositorioUsuarioWriteOnly repositorio, IMapper mapper,
    IunidadeDeTrabalho iunidadeDeTrabalho, EncriptadorDeSenha encriptadorDeSenha,
    TokenControler tokenControler, IRepositorioUsuarioReadOnly repositorioUsuarioReadOnly)
    {
         _mapper = mapper;
        _repositorio = repositorio;
        _unidadeDeTrabalho = iunidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenControler = tokenControler;
        _repositorioUsuarioReadOnly = repositorioUsuarioReadOnly;
    }
    public async Task<RespostaUsuarioRegistradoJson> Executar(RequestUsuarioJson request)
    {
        await Validar(request);
        var entidade = _mapper.Map<Domain.Entidades.Usuario>(request);
        entidade.Senha = _encriptadorDeSenha.Criptografar(request.Senha);
        await _repositorio.Adicionar(entidade);
        await _unidadeDeTrabalho.Comit();

        var token = _tokenControler.GerarToken(entidade.Email);

        return new RespostaUsuarioRegistradoJson { Token = token };

    }

    private async Task Validar(RequestUsuarioJson request)
    {

       var validator = new RegistrarUsuarioValidator();
       var resultado = validator.Validate(request);

        var ExisteUsuarioComEmail = await _repositorioUsuarioReadOnly.ExisteUsuarioComEmail(request.Email);
        if (ExisteUsuarioComEmail)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(
                "Email", ResourceMensagensDeErro.EMAIL_JA_CADASTRADO));
        }
       if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
