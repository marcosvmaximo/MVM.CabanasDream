using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class FinalizarFestaCommandHandler : Handler<FinalizarFestaCommand, FinalizarFestaViewModel>
{
    public FinalizarFestaCommandHandler(IMediatorHandler mediator) : base(mediator)
    {
    }

    public override Task<FinalizarFestaViewModel?> Handle(FinalizarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}