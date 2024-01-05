using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class ConfirmarFestaCommandHandler : Handler<ConfirmarFestaCommand, ConfirmarFestaViewModel>
{
    public ConfirmarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<ConfirmarFestaViewModel?> Handle(ConfirmarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}