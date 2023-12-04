using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festa.Domain.Entities;
using MVM.CabanasDream.Festa.Domain.Enum;

namespace MVM.CabanasDream.Festa.Domain;

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
    }

    public int QuantidadeParticipantes { get; private set; }
    public DateTime DataRealizacao { get; private set; }
    public DateTime DataRetirada { get; private set; }
    public DateTime DataDevolucao { get; private set; }
    public EStatusFesta Status { get; private set; }
    public string? Observacao { get; private set; }
    public Guid TemaId { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid ContratoId { get; private set; }
    public Guid AdministradorId { get; private set; }
    
    public Tema Tema { get; private set; }
    public Cliente Cliente { get; private set; }
    public Contrato Contrato { get; private set; }
    public Administrador Administrador { get; private set; }

    public void AdicionarContrato(Contrato contrato)
    {
        if (contrato == null)
            throw new DomainException("Contrato inválido.");

        Contrato = contrato;
        ContratoId = contrato.Id;
    }
    
    public override void Validar()
    {
        AssertionConcern.AssertArgumentRange(QuantidadeParticipantes, 1, 20, "A quantidade de participantes deve ser entre 1 e 20 participantes.");
        
        AssertionConcern.AssertArgumentGreaterThan(DataRealizacao, DateTime.Now, "A data de realização da festa deve ser maior que a data atual.");
        AssertionConcern.AssertArgumentGreaterThanOrEqualTo(DataRetirada, DateTime.Now, "A data de retirada da festa deve ser maior ou igual à data atual.");
        AssertionConcern.AssertArgumentGreaterThan(DataDevolucao, DataRetirada, "A data de devolução da festa deve ser maior que a data de retirada.");

        AssertionConcern.AssertArgumentNotNull(Tema, "A festa deve ter um tema selecionado.");
        AssertionConcern.AssertArgumentNotNull(Cliente, "A festa deve estar associada a um cliente.");
        AssertionConcern.AssertArgumentNotNull(Administrador, "A festa deve estar associada a um administrador.");

        // Observacao
        if (Observacao != null)
        {
            AssertionConcern.AssertArgumentLength(Observacao, 0, 500, "A observação da festa não deve conter mais que 500 caracteres.");
        }
        
        // Tema
        AssertionConcern.AssertArgumentNotNull(Tema, "O tema da festa deve ser selecionado.");
        AssertionConcern.AssertStateFalse(TemaId.Equals(Guid.Empty), "O tema da festa deve ser selecionado.");

        // Cliente
        AssertionConcern.AssertArgumentNotNull(Cliente, "A festa deve estar associada a um cliente.");
        AssertionConcern.AssertStateFalse(ClienteId.Equals(Guid.Empty), "A festa deve estar associada a um cliente.");

        // Contrato
        AssertionConcern.AssertArgumentNotNull(Contrato, "A festa deve estar associada a um contrato.");
        AssertionConcern.AssertStateFalse(ContratoId.Equals(Guid.Empty), "A festa deve estar associada a um contrato.");

        // Administrador
        AssertionConcern.AssertArgumentNotNull(Administrador, "A festa deve estar associada a um administrador.");
        AssertionConcern.AssertStateFalse(AdministradorId.Equals(Guid.Empty), "A festa deve estar associada a um administrador.");
    }
}