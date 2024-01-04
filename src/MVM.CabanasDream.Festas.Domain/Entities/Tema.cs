using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festas.Domain.Entities;

public class Tema : Entity
{
    private List<Produto> _produtos = new();
    private List<Festa> _festas = new();

    public Tema(string nome, decimal precoBase, string? descricao)
    {
        Nome = nome;
        Descricao = descricao;
        PrecoBase = precoBase;
        Disponibilidade = true;
        
        Validar();
    }
    
    protected Tema(){}

    public string Nome { get; private set; }
    public decimal PrecoBase { get; private set; }
    public bool Disponibilidade { get; private set; }
    public string? Descricao { get; private set; }
    public IReadOnlyCollection<Produto> Produtos => _produtos;
    public IReadOnlyCollection<Festa> Festas => _festas;

    public void AdicionarProdutoExtra(Produto produto)
    {
        if (!Disponibilidade)
            throw new DomainException("Tema indisponível.");
        
        if(produto == null)
            throw new DomainException("Produto informado inválido.");

        PrecoBase += produto.ValorLocacao;
        
        _produtos.Add(produto);
    }
    
    public void AdicionarProdutoExtra(IEnumerable<Produto> produtos)
    {
        if(produtos == null)
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
        // Nome
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do tema deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do tema não deve conter mais que 100 caracteres.");

        // Descrição
        if (Descricao != null)
        {
            AssertionConcern.AssertArgumentLength(Descricao, 0, 500, "A descrição do tema não deve conter mais que 500 caracteres.");
        }

        // Preço base
        AssertionConcern.AssertArgumentRange(PrecoBase, 1, 10000, "O preço base do tema deve estar entre R$1 e R$10.000.");
    }
}