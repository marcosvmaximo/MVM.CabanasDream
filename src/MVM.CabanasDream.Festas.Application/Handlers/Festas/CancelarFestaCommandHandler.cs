using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands.Festas;
using MVM.CabanasDream.Festas.Application.ViewModels.Festas;

namespace MVM.CabanasDream.Festas.Application.Handlers.Festas;

public class CancelarFestaCommandHandler : Handler<CancelarFestaCommand>
{
    public CancelarFestaCommandHandler(IMessageBus bus) : base(bus)
    {
    }

    public override Task<CommandResponse> Handle(CancelarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}