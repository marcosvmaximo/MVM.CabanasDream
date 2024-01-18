using FluentValidation;
using MVM.CabanasDream.Festas.Application.Commands.Temas;

namespace MVM.CabanasDream.Festas.Application.FluentValidations.Temas;

public class CriarTemaCommandValidator : AbstractValidator<CriarTemaCommand>
{
    public CriarTemaCommandValidator()
    {
        RuleFor(command => command.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Nome do Tema não deve ser vázio.")
            .MaximumLength(100)
            .WithMessage("O Nome do Tema não deve conter mais que 100 caracteres.");
        
        RuleFor(command => command.PrecoBase)
            .InclusiveBetween(0, 10000)
            .WithMessage("O Preço Base deve custar R$0.00 à R$10.000. Valores acima ou abaixo são inválidos");
        
        RuleFor(command => command.Imagem)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Nome da Imagem não deve ser vázio.")
            .MaximumLength(1000)
            .WithMessage("O Nome da Imagem não deve conter mais que 1000 caracteres.");

        RuleFor(command => command.ImagemUpload)
            .NotNull()
            .NotEmpty()
            .WithMessage("A Imagem não deve ser vázia, insira uma imagem válida.");
        
        RuleFor(command => command.Nome)
            .MaximumLength(500)
            .WithMessage("A descrição não deve conter mais que 500 caracteres.");

        RuleForEach(command => command.Produtos)
            .SetValidator(new CriarProdutoCommandValidator());
    }
}