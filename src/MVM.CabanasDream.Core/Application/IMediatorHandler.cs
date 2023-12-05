using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public interface IMediatorHandler
{
    Task PublicarEvento<TEvent>(TEvent evento) where TEvent : DomainEvent;
    Task<TResponse> EnviarComando<TRequest, TResponse>(TRequest command) where TRequest : Command<TResponse>;
    Task PublicarNotificacao<TNotification>(TNotification notification) where TNotification : DomainNotification;
}