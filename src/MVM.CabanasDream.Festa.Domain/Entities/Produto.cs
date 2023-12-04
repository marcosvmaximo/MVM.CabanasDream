using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festa.Domain.Enum;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Produto : Entity
{
    public Produto(string nome, ETipoProduto tipo, string numeroSerie, decimal valorCompra, decimal valorLocacao, Tema tema)
    {
        Nome = nome;
        Tipo = tipo;
        NumeroSerie = numeroSerie;
        ValorCompra = valorCompra;
        ValorLocacao = valorLocacao;
        Tema = tema;
        TemaId = tema.Id;
        
        Validar();
    }

    public string Nome { get; private set; }
    public ETipoProduto Tipo { get; private set; }
    public string NumeroSerie { get; private set; }
    public decimal ValorCompra { get; private set; }
    public decimal ValorLocacao { get; private set; }
    public Guid TemaId { get; private set; }
    public Tema Tema { get; private set; }
    

    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do produto deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do produto não deve conter mais que 100 caracteres.");

        AssertionConcern.AssertArgumentNotEmpty(NumeroSerie, "O número de serie deve ser informado.");
        AssertionConcern.AssertArgumentLength(NumeroSerie, 15, 15, "O número de série deve ter 15 dígitos.");
        
        AssertionConcern.AssertArgumentRange(ValorCompra, 0, 10000, "O valor da compra do produto deve estar entre R$1 e R$10.000..");
        AssertionConcern.AssertArgumentRange(ValorLocacao, 0, 10000, "O valor da locação do produto deve estar entre R$1 e R$10.000.");
        
        AssertionConcern.AssertArgumentNotNull(Tema, "O produto deve ter um tema selecionado");
        AssertionConcern.AssertStateFalse(TemaId.Equals(Guid.Empty), "O produto deve ter um tema selecionado");
    }
}