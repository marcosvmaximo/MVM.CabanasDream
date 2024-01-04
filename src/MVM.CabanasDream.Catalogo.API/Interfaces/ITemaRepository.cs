using MVM.CabanasDream.Catalogo.API.Models;
using MVM.CabanasDream.Core.Data;

namespace MVM.CabanasDream.Catalogo.API.Interfaces;

public interface ITemaRepository : IRepository
{
    Task CadastrarTema(Tema tema);
    Task<IEnumerable<Tema?>> ObterTodosTemas();
    Task<Tema?> ObterTemaPorId(Guid id);
    Task<Produto?> ObterProdutoPorId(Guid id);
}