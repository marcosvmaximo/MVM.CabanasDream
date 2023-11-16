using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Produto : Entity
{
    public Produto(string nome, decimal valorCompra, Tema tema)
    {
        Nome = nome;
        ValorCompra = valorCompra;

        if (tema == null)
            throw new DomainException("Tema informado não é valido.");
        
        Tema = tema;
        TemaId = tema.Id;
        
        Validar();
    }

    public string Nome { get; private set; }
    public decimal ValorCompra { get; private set; }
    public Guid TemaId { get; private set; }
    public Tema Tema { get; private set; }
    

    public sealed override void Validar()
    {        
        AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome do produto não pode ser vazio.");
        AssertionConcern.AssertArgumentNotNull(Nome, "Nome do produto não pode ser nulo.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "Nome do produto não deve conter mais que 100 caracteres.");

        AssertionConcern.AssertArgumentRange(ValorCompra, 0, decimal.MaxValue, "Valor da compra do produto deve ser maior que zero.");
        AssertionConcern.AssertArgumentRange(ValorCompra, 0, 10000, "Valor da compra do produto não deve ser maior que R$10.000.");
    }
}