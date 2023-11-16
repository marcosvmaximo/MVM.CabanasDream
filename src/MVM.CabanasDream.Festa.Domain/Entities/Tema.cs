using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Tema : Entity
{
    private List<Produto> _produtosBase;
    private List<Festa> _festas;

    public Tema(string nome, decimal precoBase, string? descricao)
    {
        Nome = nome;
        Descricao = descricao;
        PrecoBase = precoBase;
        Disponibilidade = true;
        
        _produtosBase = new List<Produto>();
        _festas = new List<Festa>();
        
        Validar();
    }

    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal PrecoBase { get; private set; }
    public bool Disponibilidade { get; private set; }
    public IReadOnlyCollection<Produto> ProdutosBase => _produtosBase;
    public IReadOnlyCollection<Festa> Festas => _festas;

    public void AdicionarProdutoExtra(Produto produto)
    {
        if (!Disponibilidade)
            throw new DomainException("Tema indisponível.");
        
        if(produto == null)
            throw new DomainException("Produto informado inválido.");

        _produtosBase.Add(produto);
    }
    
    public void AdicionarProdutoExtra(IEnumerable<Produto> produtos)
    {
        if(produtos == null || !produtos.Any())
            throw new DomainException("Produto informado inválido");
        
        foreach (var produto in produtos)
        {
            AdicionarProdutoExtra(produto);
        }
    }

    public void Indisponibilizar()
    {
        Disponibilidade = false;
    }

    public void Disponibilizar()
    {
        Disponibilidade = true;
    }
    
    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome do tema não pode ser vazio.");
        AssertionConcern.AssertArgumentNotNull(Nome, "Nome do tema não pode ser nulo.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "Nome não deve conter mais que 100 caracteres.");
        AssertionConcern.AssertArgumentLength(Descricao, 0, 500, "Descrição não deve conter mais que 500 caracteres.");

        AssertionConcern.AssertArgumentRange(PrecoBase, 0, decimal.MaxValue, "Preço base do tema deve ser maior que zero.");
        AssertionConcern.AssertArgumentRange(PrecoBase, 0, 10000, "Preço base do tema não deve ser maior que R$10.000.");
    }
}