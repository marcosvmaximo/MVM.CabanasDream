using MediatR;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Bus;

public class MessageBus : IMessageBus
{
    private readonly IMediator _mediator;

    public MessageBus(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task PublishEvent<TEvent>(TEvent @event) 
        where TEvent : Event
    {
        await _mediator.Publish(@event);
    }

    public async Task PublishNotification<TNotification>(TNotification notification) 
        where TNotification : INotification
    {
        await _mediator.Publish(notification);
    }

    public async Task<CommandResult> SendCommand<TRequest>(TRequest command) 
        where TRequest : Command
    {
        return await _mediator.Send(command);
    }
}