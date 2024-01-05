using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class FinalizarFestaCommandHandler : Handler<FinalizarFestaCommand, FinalizarFestaViewModel>
{
    public FinalizarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<FinalizarFestaViewModel?> Handle(FinalizarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}