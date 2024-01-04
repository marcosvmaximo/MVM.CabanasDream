using MediatR;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public class NotificationHandler : NotificationHandler<DomainNotification>, INotificationHandler
{
    private List<DomainNotification> _notifications = new();
    
    protected override void Handle(DomainNotification notification)
    {
        _notifications.Add(notification);
    }

    public async Task<bool> AnyNotifications()
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