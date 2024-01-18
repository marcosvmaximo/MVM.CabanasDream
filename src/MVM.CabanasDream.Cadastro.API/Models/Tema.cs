using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MVM.CabanasDream.Core.Domain;

namespace MVM.CabanasDream.Cadastro.API.Models;

public class Tema : IAggregateRoot
{
    private List<Produto> _produtos;
    
    public Tema(string nome, string descricao, decimal precoBase, bool disponibilidade, string imagem, string imagemUpload)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        PrecoBase = precoBase;
        Disponibilidade = disponibilidade;
        Imagem = imagem;
        ImagemUpload = imagemUpload;
        _produtos = new List<Produto>();
    }
    
    protected Tema(){}
    
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal PrecoBase { get; private set; }
    public bool Disponibilidade { get; private set; }
    public string Imagem { get; private set; }
    public string ImagemUpload { get; private set; }
    public IEnumerable<Produto> Produtos => _produtos;

    public void AdicionarProduto(Produto produto)
    {
        _produtos.Add(produto);
    }
}