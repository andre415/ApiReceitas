using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Requests;
using MeuLivroDeReceitas.Excepetions;
using System.Text.RegularExpressions;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;

public class RegistrarUsuarioValidator: AbstractValidator<RequestUsuarioJson>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(c => c.Nome).NotEmpty().WithMessage(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO);
        RuleFor(c => c.Senha).SetValidator(new SenhaValidator());
        RuleFor(c => c.Telefone).NotEmpty().WithMessage(ResourceMensagensDeErro.TELEFONE_USUARIO_EM_BRANCO);
        When(c => !string.IsNullOrWhiteSpace(c.Email),() =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO);
        });
        When(c => !string.IsNullOrWhiteSpace(c.Telefone), () =>
        {
        RuleFor(c => c.Telefone).Custom((telefone , contexto)=>{
            string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
            var isMatch = Regex.IsMatch(telefone, padraoTelefone);
            if (!isMatch)
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
            }
        });
        });
    }
}
