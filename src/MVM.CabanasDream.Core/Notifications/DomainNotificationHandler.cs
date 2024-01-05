using MediatR;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = new();
    }

    public async Task Handle(DomainNotification message, CancellationToken cancellationToken)
    {
        _notifications.Add(message);
    }
    
    public async Task<bool> AnyNotification()
    {
        return _notifications.Any();
    }

    public async Task<IEnumerable<DomainNotification>> GetNotifications()
    {
        return _notifications;
    }

    public void Dispose()
    {
        _notifications = new();
    }
}