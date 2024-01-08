using System.Linq.Expressions;
using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Domain.Interfaces;

public interface IFestaRepository : IRepository<Festa>
{
    Task<Cliente?> ObterClientePorId(Guid clienteId);
    Task<Administrador?> ObterAdministradorPorId(Guid administradorId);
    Task<Tema?> ObterTemaPorId(Guid temaId);
    
    Task<IEnumerable<Festa?>> ObterTodasFestas();
    Task<IEnumerable<Festa?>> ObterFestasPorFiltro(Expression<Func<Festa, bool>> filtro);
    Task<IEnumerable<Festa?>> ObterFestaPorCliente(Guid idCliente);
    Task SalvarFesta(Festa festa);
    Task<Festa?> ObterFestaPorId(Guid festaId);

}