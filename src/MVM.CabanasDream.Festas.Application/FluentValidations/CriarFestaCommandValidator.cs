using FluentValidation;
using MVM.CabanasDream.Festas.Application.Commands;
using System;

namespace MVM.CabanasDream.Festas.Application.Validators
{
    public class CriarFestaCommandValidator : AbstractValidator<CriarFestaCommand>
    {
        public CriarFestaCommandValidator()
        {
            RuleFor(command => command.TemaId).NotEmpty().WithMessage("O ID do tema deve ser fornecido.");
            RuleFor(command => command.ClienteId).NotEmpty().WithMessage("O ID do cliente deve ser fornecido.");
            RuleFor(command => command.AdministradorId).NotEmpty().WithMessage("O ID do administrador deve ser fornecido.");

            RuleFor(command => command.DataRetirada)
                .GreaterThan(DateTime.Now)
                .WithMessage("A data de retirada da festa deve ser maior que a data atual.");

            RuleFor(command => command.DataRealizacao)
                .GreaterThan(c => c.DataRetirada)
                .WithMessage("A data de realização da festa deve ser maior que a data atual.");

            RuleFor(command => command.DataDevolucao)
                .GreaterThan(c => c.DataRealizacao)
                .WithMessage("A data de devolução da festa deve ser maior que a data de realização.");

            RuleFor(command => command.QuantidadeParticipantes)
                .InclusiveBetween(1, 20)
                .WithMessage("A quantidade de participantes deve ser entre 1 e 20.");

            RuleFor(command => command.Observacao)
                .MaximumLength(500)
                .WithMessage("A observação da festa não deve conter mais que 500 caracteres.");
        }
    }
}