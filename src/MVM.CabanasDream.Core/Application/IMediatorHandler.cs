using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Application;

public interface IMediatorHandler
{
    Task PublicarEvento<TEvent>(TEvent evento) 
        where TEvent : Event;
    Task<TResponse?> EnviarComando<TRequest, TResponse>(TRequest command) 
        where TRequest : Command<TResponse>;
    Task PublicarNotificacao<TNotification>(TNotification notification) 
        where TNotification : DomainNotification;
}