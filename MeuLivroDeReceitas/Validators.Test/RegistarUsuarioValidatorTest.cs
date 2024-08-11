using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Excepetions;
using Utilitario.ParaTestes.Requests;
using Xunit;
namespace Validators.Test;

public class RegistarUsuarioValidatorTest
{
    [Fact]
    public void Validar_Susesso()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_Nome_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Nome = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Email = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazia()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Senha = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Telefone = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Email = "we";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir();
        request.Telefone = "973920";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalida(int tamanhoSenha)
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistarUsuarioBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));
    }

}
