using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class CancelarFestaCommandHandler : Handler<CancelarFestaCommand, CancelarFestaViewModel>
{
    public CancelarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<CancelarFestaViewModel?> Handle(CancelarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}