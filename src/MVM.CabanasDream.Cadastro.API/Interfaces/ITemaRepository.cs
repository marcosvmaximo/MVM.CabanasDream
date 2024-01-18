using MVM.CabanasDream.Cadastro.API.Models;
using MVM.CabanasDream.Core.Data;

namespace MVM.CabanasDream.Cadastro.API.Interfaces;

public interface ITemaRepository : IRepository<>
{
    Task CadastrarTema(Tema tema);
    Task<IEnumerable<Tema?>> ObterTodosTemas();
    Task<Tema?> ObterTemaPorId(Guid id);
    Task<Produto?> ObterProdutoPorId(Guid id);
}