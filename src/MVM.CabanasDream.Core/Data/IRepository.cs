using MVM.CabanasDream.Core.Domain;

namespace MVM.CabanasDream.Core.Data;

public interface IRepository<TAggregate> : IDisposable 
    where TAggregate : IAggregateRoot
{
}