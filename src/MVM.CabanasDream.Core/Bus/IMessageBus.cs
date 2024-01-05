using MediatR;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Bus;

public interface IMessageBus
{
    Task PublishEvent<TEvent>(TEvent @event) 
        where TEvent : Event;
    
    Task PublishNotification<TNotification>(TNotification notification) 
        where TNotification : INotification;
    
    Task<TResponse?> SendCommand<TRequest, TResponse>(TRequest command) 
        where TRequest : Command<TResponse>;
}