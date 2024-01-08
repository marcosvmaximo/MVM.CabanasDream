using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public abstract class Handler<TCommand> : IRequestHandler<TCommand, CommandResult>
    where TCommand : Command
{
    protected IMessageBus _bus;
    protected ValidationResult ValidationResult { get; private set; }
    
    protected Handler(IMessageBus bus)
    {
        _bus = bus;
    }
    
    public abstract Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken);

    protected virtual bool ValidarComando<TValidation>(TCommand command)
        where TValidation : AbstractValidator<TCommand>, new()
    {
        ValidationResult = command.FastValidation<TCommand, TValidation>();

        foreach (var error in ValidationResult.Errors)
        {
            _bus.PublishNotification(new DomainNotification(error.PropertyName, error.ErrorMessage));
        }

        return ValidationResult.IsValid;
    }
    
    protected CommandResult CustomResponse(object data = null)
    {
        if(!ValidationResult.IsValid)
            return CommandResult.CustomResponse(ValidationResult.Errors);

        return CommandResult.CustomResponse(data);
    }

    protected void AddError(string propertyName, string errorMessage)
    {
        ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
    }
}