using System.ComponentModel.DataAnnotations;
using MVM.CabanasDream.Catalogo.API.Enum;
using MVM.CabanasDream.Core.Domain;

namespace MVM.CabanasDream.Catalogo.API.Models;

public class Produto
{
    public Produto(string nome, ETipoProduto tipo, Tema tema)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Tipo = tipo;
        Tema = tema;
        TemaId = tema.Id;
    }
    
    protected Produto(){}
    
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public ETipoProduto Tipo { get; set; }
    public Tema Tema { get; private set; }
    public Guid TemaId { get; private set; }
}