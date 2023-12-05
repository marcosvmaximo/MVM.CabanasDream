using MediatR;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task PublicarEvento<TEvent>(TEvent evento) where TEvent : DomainEvent
    {
        await _mediator.Publish(evento);
    }

    public async Task<TResponse> EnviarComando<TRequest, TResponse>(TRequest command) where TRequest : Command<TResponse>
    {
        return await _mediator.Send(command);
    }

    public async Task PublicarNotificacao<TNotification>(TNotification notification) where TNotification : DomainNotification
    {        
        await _mediator.Publish(notification);
    }
}