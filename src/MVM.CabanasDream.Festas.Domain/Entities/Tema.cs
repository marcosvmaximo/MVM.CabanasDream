using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festas.Domain.Entities;

public class Tema : Entity
{
    private List<Produto> _produtos = new();
    private List<Festa> _festas = new();

    public Tema(string nome, decimal precoBase, Imagem imagem, string? descricao)
    {
        Nome = nome;
        Descricao = descricao;
        PrecoBase = precoBase;
        Imagem = imagem;
        Disponibilidade = true;
        
        Validar();
    }
    
    protected Tema(){}

    public string Nome { get; private set; }
    public decimal PrecoBase { get; private set; }
    public Imagem Imagem { get; private set; }
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

        PrecoBase += produto.Valor.ValorLocacao;
        
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

    public void AlterarImagem(Imagem imagem)
    {
        if (imagem == null)
            throw new DomainException("Imagem informado é inválida");
        
        Imagem = imagem;
    }

    public void AlterarPreco(decimal novoPreco)
    {
        PrecoBase = novoPreco;
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
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do tema deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do tema não deve conter mais que 100 caracteres.");
        AssertionConcern.AssertArgumentNotNull(Imagem, "A imagem do Tema deve ser informada.");

        if (Descricao != null)
        {
            AssertionConcern.AssertArgumentLength(Descricao, 0, 500, "A descrição do tema não deve conter mais que 500 caracteres.");
        }

        AssertionConcern.AssertArgumentRange(PrecoBase, 1, 10000, "O preço base do tema deve estar entre R$1 e R$10.000.");
    }
}