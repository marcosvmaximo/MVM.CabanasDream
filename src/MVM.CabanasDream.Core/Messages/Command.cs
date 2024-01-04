using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Messages;

public abstract class Command<TResponse> : Event, IRequest<TResponse>
{
    public virtual ValidationResult FastValidation<TCommand, TValidate>()
        where TCommand : Command<TResponse>
        where TValidate : AbstractValidator<TCommand>, new()
    {
        TValidate validate = new();

        return validate.Validate((TCommand)this);
    }
}