using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class ConfirmarFestaCommandHandler : Handler<ConfirmarFestaCommand>
{
    private readonly IFestaRepository _repository;

    public ConfirmarFestaCommandHandler(IMessageBus bus, IFestaRepository repository) : base(bus)
    {
        _repository = repository;
    }

    public override Task<CommandResponse?> Handle(ConfirmarFestaCommand request, CancellationToken cancellationToken)
    {        
        throw new NotImplementedException();

    }
}