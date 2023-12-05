using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public interface INotificationHandler : IDisposable
{
    Task<bool> AnyNotifications();
    Task<IEnumerable<DomainNotification>> GetNotifications();
}