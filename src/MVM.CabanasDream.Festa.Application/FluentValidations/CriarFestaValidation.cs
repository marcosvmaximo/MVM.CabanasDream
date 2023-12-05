using FluentValidation;
using MVM.CabanasDream.Festa.Application.Commands;

namespace MVM.CabanasDream.Festa.Application.FluentValidations;

public class CriarFestaValidation : AbstractValidator<CriarFestaCommand>
{
    public CriarFestaValidation()
    {
    }
}