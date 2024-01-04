using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class ConfirmarFestaCommandHandler : Handler<ConfirmarFestaCommand, ConfirmarFestaViewModel>
{
    public ConfirmarFestaCommandHandler(IMediatorHandler mediator) : base(mediator)
    {
    }

    public override Task<ConfirmarFestaViewModel?> Handle(ConfirmarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}