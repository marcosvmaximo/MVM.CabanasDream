using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festas.Domain.ValueObjects;

public class DataFesta : ValueObject
{
    public DataFesta(DateTime realizacao, DateTime retirada, DateTime devolucao)
    {
        Realizacao = realizacao;
        Retirada = retirada;
        Devolucao = devolucao;
    }

    public DateTime Realizacao { get; private set; }
    public DateTime Retirada { get; private set; }
    public DateTime Devolucao { get; private set; }
    
    public override void Validar()
    {
        AssertionConcern.AssertArgumentLetterThan(Realizacao, DateTime.Now, "A data de realização da festa deve ser maior que a data atual.");
        AssertionConcern.AssertArgumentLetterThan(Retirada, DateTime.Now, "A data de retirada da festa deve ser maior ou igual à data atual.");
        AssertionConcern.AssertArgumentGreaterThan(Devolucao, Retirada, "A data de devolução da festa deve ser maior que a data de retirada.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Realizacao;
        yield return Retirada;
        yield return Devolucao;
    }
}