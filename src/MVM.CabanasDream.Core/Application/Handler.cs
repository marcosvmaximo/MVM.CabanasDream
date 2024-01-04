using FluentValidation;
using MediatR;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public abstract class Handler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : Command<TResponse>
    where TResponse : class
{
    protected internal IMediatorHandler _mediator;
    
    protected Handler(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }
    
    public abstract Task<TResponse?> Handle(TCommand request, CancellationToken cancellationToken);

    protected virtual bool ValidarComando<TValidation>(TCommand command)
        where TValidation : AbstractValidator<TCommand>, new()
    {
        var validate = command.FastValidation<TCommand, TValidation>();

        foreach (var error in validate.Errors)
        {
            var notification = new DomainNotification(error.PropertyName, error.ErrorMessage);
            _mediator.PublicarNotificacao(notification);
        }

        return validate.IsValid;
    }
}