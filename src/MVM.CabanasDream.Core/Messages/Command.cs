using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Messages;

public abstract class Command : Message, IRequest<CommandResponse>
{
    public Command()
    {
        MessageType = GetType().Name;
    }
    
    public virtual ValidationResult FastValidation<TCommand, TValidate>()
        where TCommand : Command
        where TValidate : AbstractValidator<TCommand>, new()
    {
        TValidate validate = new();

        return validate.Validate((TCommand)this);
    }
}