using MediatR;

namespace MVM.CabanasDream.Core.Messages.Common;

public abstract class Event : Message
{
    protected Event()
    {
        MessageType = GetType().Name;
    }
}