using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festas.Domain.ValueObjects;

public class Valor : ValueObject
{
    public Valor(decimal compra, decimal locacao)
    {
        ValorCompra = compra;
        ValorLocacao = locacao;
    }
    
    public decimal ValorCompra { get; private set; }
    public decimal ValorLocacao { get; private set; }
    
    public override void Validar()
    {
        AssertionConcern.AssertArgumentRange(ValorCompra, 0, 10000, "O valor da compra do produto deve estar entre R$1 e R$10.000.");
        AssertionConcern.AssertArgumentRange(ValorLocacao, 0, 10000, "O valor da locação do produto deve estar entre R$1 e R$10.000.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValorCompra;
        yield return ValorLocacao;
    }
}