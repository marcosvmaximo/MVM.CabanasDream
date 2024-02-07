using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Festas.Domain.TemaContext;
using MVM.CabanasDream.Festas.Domain.TemaContext.Entities;

namespace MVM.CabanasDream.Festas.Domain.Interfaces;

public interface ITemaRepository : IRepository<Tema>
{
    Task<Tema?> ObterPorId(Guid id);
    Task<IEnumerable<Tema>> ObterTodos();
    Task<IEnumerable<Tema>> ObterTodosPorFiltro();
    Task Adicionar(Tema tema);
    Task AdicionarProduto(Produto produto);
    // Criar contexto para o Tema, onde ele é o agregado
    // O Tema poderá ser exibido, e inserido
    // Cada tema terá uma lista de produtos ao inserir, no caso o ID do produto
    // Teremos que inserir os produtos antes, através de uma inserção a parte,
    // no qual o produto poderá existir sem o Tema, e após a criação do tema
    // o produto em questão será ligado ao Tema


    // Problemas?
    // O que sobrará no contexto de Festa?

    Task<Produto> ObterProdutoPorId(Guid idProduto);
}