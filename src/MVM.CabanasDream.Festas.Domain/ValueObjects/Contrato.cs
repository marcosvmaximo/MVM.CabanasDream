using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.Enum;

namespace MVM.CabanasDream.Festas.Domain.ValueObjects;

public class Contrato : ValueObject
{
    public Contrato(decimal valor, decimal multa)
    {
        Valor = valor;
        Multa = multa;
        Assinado = false;
        Status = EStatusContrato.Pendente;
        
        Validar();
    }
    public decimal Valor { get; private set; }
    public decimal Multa { get; private set; }
    public bool Assinado { get; private set; }
    public EStatusContrato Status { get; private set; }
    
    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentRange(Valor, 1, 10000, "O valor do contrato deve estar entre R$1 e R$10.000.");
        AssertionConcern.AssertArgumentRange(Multa, 1, 10000, "A multa do contrato deve estar entre R$1 e R$10.000.");
    }

    public void Assinar()
    {
        if (!Assinado)
            throw new DomainException("Contrato já está assinado.");
        
        Assinado = true;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
        yield return Multa;
        yield return Assinado;
        yield return Status;
    }

    public void ConfirmarContrato()
    {
        if (Status != EStatusContrato.Pendente)
            throw new DomainException("Status do contrato necessita ser pendente para ser confirmado");

        Status = EStatusContrato.Vigente;
    }

    public void QuebrarContrato()
    {
        if (Status != EStatusContrato.Pendente || Status != EStatusContrato.Vigente)
            throw new DomainException("Status do contrato necessita estar ativo para ser cancelado");

        Status = EStatusContrato.Cancelado;
    }

    public void EncerrarContrato()
    {
        if (Status != EStatusContrato.Vigente)
            throw new DomainException("Status do contrato necessita ser vigente para ser encerrado");

        Status = EStatusContrato.Encerrado;
    }

}
