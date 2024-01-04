using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class RetirarFestaCommandHandler : Handler<RetirarFestaCommand, RetirarFestaViewModel>
{
    public RetirarFestaCommandHandler(IMediatorHandler mediator) : base(mediator)
    {
    }

    public override Task<RetirarFestaViewModel?> Handle(RetirarFestaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}