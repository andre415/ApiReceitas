using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Excepetions;

namespace MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;
public class RegistrarReceitaValidator: AbstractValidator <RequestRegistrarReceitaJson>
{

    public RegistrarReceitaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().WithMessage(ResourceMensagensDeErro.RECEITA_TITULO_EM_BRANCO);
        RuleFor(x => x.Categoria).IsInEnum().WithMessage(ResourceMensagensDeErro.RECEITA_CATEGORIA_INVALIDA);
        RuleFor(x => x.TempoDePreparo).InclusiveBetween(1,1000).WithMessage(ResourceMensagensDeErro.RECEITA_TEMPO_DE_PREPARO_INVALIDO);
        RuleFor(x => x.ModoDePreparo).NotEmpty().WithMessage(ResourceMensagensDeErro.RECEITA_MODO_DE_PREPARO_EM_BRANCO);
        RuleFor(x => x.Ingredientes).NotEmpty().WithMessage(ResourceMensagensDeErro.RECEITA_INGREDIENTE_EM_BRANCO);
        RuleForEach(x => x.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(x => x.Produto).NotEmpty().WithMessage(ResourceMensagensDeErro.RECEITA_INGREDIENTE_PRODUTO_EM_BRANCO);
            ingrediente.RuleFor(x => x.Quantidade).NotEmpty().WithMessage(ResourceMensagensDeErro.RECEITA_INGREDIENTE_QUANTIDADE_EM_BRANCO);
        });
        RuleFor(x => x.Ingredientes).Custom((ingredientes, contexto) =>
        {
            var produtosDistintos = ingredientes.Select(c => c.Produto).Distinct();
            if(produtosDistintos.Count() != ingredientes.Count())
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure("ingredientes", ""));
            }

        });
    }
}
