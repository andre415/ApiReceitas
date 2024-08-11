using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Excepetions;
using System.Collections.Generic;
using Utilitario.ParaTestes.Requests;

namespace Validators.Test;
public class RegistrarReceitaValidatorTeste
{
    [Fact]
    public void Validar_Susesso()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();
        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeTrue();
    }
    [Fact]
    public void Validar_Erro_Titulo_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();

        request.Titulo = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.RECEITA_TITULO_EM_BRANCO));

    }
    [Fact]
    public void Validar_Erro_ModoDePreparo_Vazio()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();

        request.ModoDePreparo = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.RECEITA_MODO_DE_PREPARO_EM_BRANCO));

    }
    [Fact]
    public void Validar_Erro_Categoria_Invalida()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();

        request.Categoria = (MeuLivroDeReceitas.Comunicacao.Enum.Categoria)11;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.RECEITA_CATEGORIA_INVALIDA));

    }
    [Fact]
    public void Validar_Erro_Ingrediente_Em_Branco()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();

        request.Ingredientes = new List<RequestRegistrarIngredientesJson>();

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.RECEITA_INGREDIENTE_EM_BRANCO));

    }
    [Fact]
    public void Validar_Erro_Produto_Em_Branco()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();

        request.Ingredientes.Add(new RequestRegistrarIngredientesJson
        {
            Quantidade = "2",
            Produto = string.Empty
        });
        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.RECEITA_INGREDIENTE_PRODUTO_EM_BRANCO));

    }
    public void Validar_Erro_Quantidade_Em_Branco()
    {
        var validator = new RegistrarReceitaValidator();

        var request = RequestReceitaJsonBuilder.Construir();

        request.Ingredientes.Add(new RequestRegistrarIngredientesJson
        {
            Quantidade = string.Empty,
            Produto = "teste"
        });
        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And
        .Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.RECEITA_INGREDIENTE_QUANTIDADE_EM_BRANCO));

    }
}
