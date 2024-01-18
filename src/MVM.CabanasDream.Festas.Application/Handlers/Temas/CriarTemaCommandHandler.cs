using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands.Temas;

namespace MVM.CabanasDream.Festas.Application.Handlers.Temas;

public class CriarTemaCommandHandler : Handler<CriarTemaCommand>
{
    public CriarTemaCommandHandler(IMessageBus messager) : base(messager)
    {
    }

    public override Task<CommandResponse> Handle(CriarTemaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}