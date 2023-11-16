using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festa.Domain.Entities;

namespace MVM.CabanasDream.Festa.Domain;

public class Festa : Entity, IAggregateRoot
{

    public Festa(int quantidadeParticipantes, DateTime dataRetirada, DateTime dataEntrega, DateTime dataRealizacao,
        Tema tema, Cliente cliente)
    {
        QuantidadeParticipantes = quantidadeParticipantes;
        DataRetirada = dataRetirada;
        DataEntrega = dataEntrega;
        DataRealizacao = dataRealizacao;
        Tema = tema;
        Cliente = cliente;
        TemaId = tema.Id; 
        ClienteId = cliente.Id;
    }

    public int QuantidadeParticipantes { get; private set; }
    public DateTime DataRetirada { get; private set; }
    public DateTime DataEntrega { get; private set; }
    public DateTime DataRealizacao { get; private set; }
    public Guid TemaId { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid ContratoId { get; private set; }
    
    public Tema Tema { get; private set; }
    public Cliente Cliente { get; private set; }
    public Contrato Contrato { get; private set; }

    public void AdicionarContrato(Contrato contrato)
    {
        if (contrato == null)
            throw new DomainException("Contrato inválido.");

        Contrato = contrato;
        ContratoId = contrato.Id;
    }
    
    public override void Validar()
    {
        throw new NotImplementedException();
    }
}