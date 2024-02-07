using System.Linq.Expressions;
using MVM.CabanasDream.Core.Data;

namespace MVM.CabanasDream.Festas.Domain.FestaContext.Interfaces;

public interface IFestaRepository : IRepository<Festa>
{
    Task<IEnumerable<Festa?>> ObterTodasFestas();
    Task<IEnumerable<Festa?>> ObterFestasPorFiltro(Expression<Func<Festa, bool>> filtro);
    Task<IEnumerable<Festa>> ObterFestaPorCliente(Guid idCliente);
    Task SalvarFesta(Festa festa);
    Task<Festa?> ObterFestaPorId(Guid festaId);
}