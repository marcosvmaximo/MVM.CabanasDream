using FluentValidation;
using MediatR;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public abstract class Handler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : Command<TResponse>
    where TResponse : class
{
    protected IMessageBus _bus;
    
    protected Handler(IMessageBus bus)
    {
        _bus = bus;
    }
    
    public abstract Task<TResponse?> Handle(TCommand request, CancellationToken cancellationToken);

    protected virtual bool ValidarComando<TValidation>(TCommand command)
        where TValidation : AbstractValidator<TCommand>, new()
    {
        var validate = command.FastValidation<TCommand, TValidation>();

        foreach (var error in validate.Errors)
        {
            _bus.PublishNotification(new DomainNotification(error.PropertyName, error.ErrorMessage));
        }

        return validate.IsValid;
    }
}