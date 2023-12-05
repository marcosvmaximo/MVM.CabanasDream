using MediatR;

namespace MVM.CabanasDream.Core.Messages.Common;

public abstract class Event : Message, INotification
{
    protected Event()
    {
    }
}