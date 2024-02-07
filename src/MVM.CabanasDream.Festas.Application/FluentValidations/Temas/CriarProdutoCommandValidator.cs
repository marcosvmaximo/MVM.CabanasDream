using FluentValidation;
using MVM.CabanasDream.Festas.Application.Commands.Temas;

namespace MVM.CabanasDream.Festas.Application.FluentValidations.Temas;

public class CriarProdutoCommandValidator : AbstractValidator<CriarProdutoCommand>
{
    public CriarProdutoCommandValidator()
    {
        RuleFor(command => command.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Nome do Produto não deve ser vázio.")
            .MaximumLength(100)
            .WithMessage("O Nome do Produto não deve conter mais que 100 caracteres.");

        RuleFor(command => command.ValorCompra)
            .InclusiveBetween(0, 10000)
            .WithMessage("O Valor da Compra do Produto deve custar R$0.00 à R$10.000. Valores acima ou abaixo são inválidos");

        RuleFor(command => command.ValorLocacao)
            .InclusiveBetween(0, 10000)
            .WithMessage("O Valor da Locação do Produto deve custar R$0.00 à R$10.000. Valores acima ou abaixo são inválidos");

    }
}