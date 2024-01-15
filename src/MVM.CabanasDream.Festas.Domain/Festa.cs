using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Enum;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festas.Domain;

public class Festa : Entity, IAggregateRoot
{
    public Festa(int quantidadeParticipantes, DateTime dataRealizacao, DateTime dataRetirada, DateTime dataDevolucao,
        Tema tema, Cliente cliente, Administrador administrador, string? observacao = null)
    {
        QuantidadeParticipantes = quantidadeParticipantes;
        DataRetirada = dataRetirada;
        DataRealizacao = dataRealizacao;
        DataDevolucao = dataDevolucao;
        Tema = tema;
        Cliente = cliente;
        TemaId = tema.Id; 
        ClienteId = cliente.Id;
        Administrador = administrador;
        AdministradorId = administrador.Id;

        Status = EStatusFesta.Pendente;
        Observacao = observacao;
        
        Validar();
    }

    protected Festa(){}
    
    public int QuantidadeParticipantes { get; private set; }
    public DateTime DataRealizacao { get; private set; }
    public DateTime DataRetirada { get; private set; }
    public DateTime DataDevolucao { get; private set; }
    public EStatusFesta Status { get; private set; }
    public Contrato Contrato { get; private set; }
    public string? Observacao { get; private set; }
    public Guid TemaId { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid AdministradorId { get; private set; }
    public Tema Tema { get; private set; }
    public Cliente Cliente { get; private set; }
    public Administrador Administrador { get; private set; }

    public void ConfirmarFesta()
    {
        if (Status is not EStatusFesta.Pendente)
            throw new DomainException("A festa precisa estar pendente para ser confirmado.");

        if(Contrato is null)           
            throw new DomainException("A festa necessita de um contrato alocado para realizar essa ação.");
        
        Contrato.ConfirmarContrato();
        Status = EStatusFesta.EmAndamento;
    }

    public decimal CancelarFesta()
    {
        if (Status is EStatusFesta.Concluida)
            throw new DomainException("Não é possível cancelar uma festa já concluída.");

        if (Status is EStatusFesta.Cancelada)
            throw new DomainException("Não é possível cancelar uma festa já cancelada.");
        
        if(Contrato is null)
            throw new DomainException("A festa necessita de um contrato alocado para realizar essa ação.");

        Status = EStatusFesta.Cancelada;
        Contrato.QuebrarContrato();
        
        return ObterValorMulta();
    }

    public decimal ObterValorFesta()
    {
        // Preço base
        decimal precoBase = Tema.PrecoBase;

        // + Quantidade de participantes
        decimal precoExtraPorParticipante = (precoBase * 0.1m) * QuantidadeParticipantes;

        return precoBase + precoExtraPorParticipante;
    }
    
    public decimal ObterValorMulta()
    {
        return ObterValorFesta() * 0.15m;
    }

    public void AssociarContrato(Contrato contrato)
    {
        AssertionConcern.AssertArgumentNotNull(Contrato, "A festa deve possuir um Contrato associado.");

        Contrato = contrato;
    }
    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentRange(QuantidadeParticipantes, 1, 20, "A quantidade de participantes deve ser entre 1 e 20 participantes.");
        AssertionConcern.AssertArgumentLetterThan(DataRealizacao, DateTime.Now, "A data de realização da festa deve ser maior que a data atual.");
        AssertionConcern.AssertArgumentLetterThan(DataRetirada, DateTime.Now, "A data de retirada da festa deve ser maior ou igual à data atual.");
        AssertionConcern.AssertArgumentGreaterThan(DataDevolucao, DataRetirada, "A data de devolução da festa deve ser maior que a data de retirada.");
        
        AssertionConcern.AssertArgumentNotNull(Tema, "A festa deve possuir um Tema associado.");
        
        AssertionConcern.AssertArgumentNotNull(Cliente, "A festa deve possuir um Cliente associado.");
        
        AssertionConcern.AssertArgumentNotNull(Administrador, "A festa deve possuir um Administrador associado.");
        
        if (!string.IsNullOrWhiteSpace(Observacao))
            AssertionConcern.AssertArgumentLength(Observacao, 0, 500, "A observação da festa não deve conter mais que 500 caracteres.");
    }
}