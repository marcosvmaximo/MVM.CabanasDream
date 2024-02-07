using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Festas.Domain.Interfaces;
using MVM.CabanasDream.Festas.Domain.TemaContext;
using MVM.CabanasDream.Festas.Domain.TemaContext.Entities;

namespace MVM.CabanasDream.Festas.Data.Repositories;

public class TemaRepository : ITemaRepository
{
    public IUnityOfWork UnityOfWork { get; }
    
    public Task<Tema?> ObterPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tema>> ObterTodos()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tema>> ObterTodosPorFiltro()
    {
        throw new NotImplementedException();
    }

    public Task Adicionar(Tema tema)
    {
        throw new NotImplementedException();
    }

    public Task AdicionarProduto(Produto produto)
    {
        throw new NotImplementedException();
    }

    public Task<Produto> ObterProdutoPorId(Guid idProduto)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        // TODO release managed resources here
    }
}