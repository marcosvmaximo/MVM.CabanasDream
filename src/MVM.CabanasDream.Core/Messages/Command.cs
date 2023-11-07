using MediatR;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Messages;

public abstract class Command<TResponse> : Event, IRequest<TResponse>
{
    public virtual void FastValidation()
    {
        throw new NotImplementedException();
    }
}