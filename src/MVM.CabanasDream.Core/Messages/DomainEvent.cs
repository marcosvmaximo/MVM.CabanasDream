using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Messages;

public class DomainEvent : Event
{
    public Guid AggregateId { get; init; }
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
        MessageType = GetType().Name;
    }
}