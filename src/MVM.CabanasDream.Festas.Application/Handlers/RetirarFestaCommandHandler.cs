using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class RetirarFestaCommandHandler : Handler<RetirarFestaCommand>
{
    public RetirarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<CommandResult> Handle(RetirarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}