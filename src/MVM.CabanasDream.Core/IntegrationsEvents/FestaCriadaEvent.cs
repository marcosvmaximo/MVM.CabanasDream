using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.IntegrationsEvents;

public class FestaCriadaEvent : DomainEvent
{
    public FestaCriadaEvent(Guid aggregateId, Guid temaId, Guid clienteId, Guid administradorId) : base(aggregateId)
    {
        TemaId = temaId;
        ClienteId = clienteId;
        AdministradorId = administradorId;
    }

    public Guid TemaId { get; set; }
    public Guid ClienteId { get; set; }
    public Guid AdministradorId { get; set; }
}