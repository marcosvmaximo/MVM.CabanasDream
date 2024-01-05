using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Domain.Interfaces;

public interface IFestaRepository : IRepository<Festa>
{
    Task<Cliente?> ObterClientePorId(Guid requestClienteId);
    Task<Administrador?> ObterAdministradorPorId(Guid requestAdministradorId);
    Task<Tema?> ObterTemaPorId(Guid temaId);
    Task<Festa?> ObterFestaPorId(Guid festaId);
    Task SalvarFesta(Festa festa);
}