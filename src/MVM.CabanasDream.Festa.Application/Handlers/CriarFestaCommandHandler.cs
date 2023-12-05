using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Festa.Application.Commands;
using MVM.CabanasDream.Festa.Application.FluentValidations;
using MVM.CabanasDream.Festa.Application.ViewModels;

namespace MVM.CabanasDream.Festa.Application.Handlers;

public class CriarFestaCommandHandler : Handler<CriarFestaCommand, CriarFestaViewModel>
{
    public CriarFestaCommandHandler(IMediatorHandler mediator) : base(mediator)
    {
    }

    public override async Task<CriarFestaViewModel> Handle(CriarFestaCommand request, CancellationToken cancellationToken)
    {
        if (!await ValidarComando<CriarFestaValidation>(request))
        {
            return null;
        }
        
    }
}