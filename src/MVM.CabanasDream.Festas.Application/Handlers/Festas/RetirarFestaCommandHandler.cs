using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.Commands.Festas;
using MVM.CabanasDream.Festas.Application.ViewModels.Festas;

namespace MVM.CabanasDream.Festas.Application.Handlers.Festas;

public class RetirarFestaCommandHandler : Handler<RetirarFestaCommand>
{
    public RetirarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<CommandResponse> Handle(RetirarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}