using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands.Festas;
using MVM.CabanasDream.Festas.Application.ViewModels.Festas;

namespace MVM.CabanasDream.Festas.Application.Handlers.Festas;

public class FinalizarFestaCommandHandler : Handler<FinalizarFestaCommand>
{
    public FinalizarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<CommandResponse> Handle(FinalizarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}