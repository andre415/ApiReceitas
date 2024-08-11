using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Excepetions;
using Utilitario.ParaTestes.Requests;

namespace Validators.Test;

public class AlterarSenhaValidatorTeste
{
    [Fact]
    public void Validar_Susesso()
    {
        var validator = new SenhaValidator();

        var request = RequestAlterarSenhaBuilder.Construir();
        var resultado = validator.Validate(request.NovaSenha);

        resultado.IsValid.Should().BeTrue();
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalida(int tamanhoSenha)
    {
        var validator = new SenhaValidator();

        var request = RequestAlterarSenhaBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(request.NovaSenha);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazia()
    {
        var validator = new SenhaValidator();

        var request = RequestAlterarSenhaBuilder.Construir();
        request.NovaSenha = string.Empty;

        var resultado = validator.Validate(request.NovaSenha);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));
    }
}
