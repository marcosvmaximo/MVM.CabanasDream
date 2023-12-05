using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festa.Domain.Enum;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Contrato : Entity
{
    public Contrato(decimal valor, decimal multa, EStatusContrato status, Festa festa)
    {
        Valor = valor;
        Multa = multa;
        Status = status;
        FestaId = festa.Id;
        Festa = festa;
        
        Validar();
    }
    public decimal Valor { get; private set; }
    public decimal Multa { get; private set; }
    public EStatusContrato Status { get; private set; }
    public Guid FestaId { get; private set; }
    public Festa Festa { get; private set; }
    
    public sealed override void Validar()
    {
        // Valor
        AssertionConcern.AssertArgumentRange(Valor, 1, 10000, "O valor do contrato deve estar entre R$1 e R$10.000.");

        // Festa
        AssertionConcern.AssertArgumentNotNull(Festa, "O contrato deve estar associado a uma festa.");
        AssertionConcern.AssertStateFalse(FestaId.Equals(Guid.Empty), "O ID da festa associada ao contrato não pode ser nulo.");
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
