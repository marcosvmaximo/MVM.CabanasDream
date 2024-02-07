using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.FestaContext.Enum;
using MVM.CabanasDream.Festas.Domain.FestaContext.ValueObjects;
using MVM.CabanasDream.Festas.Domain.TemaContext;

namespace MVM.CabanasDream.Festas.Domain.FestaContext;

public class Festa : Entity, IAggregateRoot
{
    public Festa(
        int quantidadeParticipantes,
        Tema tema,
        DataFesta data,
        Guid clienteId,
        Guid administradorId,
        string? observacao = null)
    {
        Tema = tema;
        QuantidadeParticipantes = quantidadeParticipantes;
        Status = EStatusFesta.Pendente;
        Data = data;
        Observacao = observacao;

        ClienteId = clienteId;
        AdministradorId = administradorId;

        if(Tema is not null)
            TemaId = tema.Id;
        
        Validar();
    }

    protected Festa(){}
    
    public Tema Tema { get; private set; }
    public EStatusFesta Status { get; private set; }
    public int QuantidadeParticipantes { get; private set; }
    public DataFesta Data { get; set; }
    public string? Observacao { get; private set; }
    public Guid TemaId { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid AdministradorId { get; private set; }
     
    public void ConfirmarFesta()
    {
        if (Status is not EStatusFesta.Pendente)
            throw new DomainException("A festa precisa estar pendente para ser confirmado.");

        Status = EStatusFesta.EmAndamento;
    }

    public decimal CancelarFesta()
    {
        if (Status is EStatusFesta.Concluida)
            throw new DomainException("Não é possível cancelar uma festa já concluída.");

        if (Status is EStatusFesta.Cancelada)
            throw new DomainException("Não é possível cancelar uma festa já cancelada.");
        
        Status = EStatusFesta.Cancelada;
        // Mandar evento para quebra de contato
        return 0;
    }

    public decimal ObterValorFesta()
    {
        // Preço base
        decimal precoBase = Tema.PrecoBase;

        // + Quantidade de participantes
        decimal precoExtraPorParticipante = (precoBase * 0.1m) * QuantidadeParticipantes;

        return precoBase + precoExtraPorParticipante;
    }

    public void AssociarContrato()
    {
    }
    
    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentNotNull(Tema, "A festa deve possuir um Tema associado.");
        AssertionConcern.AssertArgumentNotEquals(TemaId, Guid.Empty,"Id do Tema inválido.");
        AssertionConcern.AssertArgumentNotEquals(ClienteId, Guid.Empty,"Id do Cliente inválido.");
        AssertionConcern.AssertArgumentNotEquals(AdministradorId, Guid.Empty,"Id do Administrador inválido");
        
        AssertionConcern.AssertArgumentNotNull(Data, "A festa deve possuir um Datas válidas.");
        AssertionConcern.AssertArgumentRange(QuantidadeParticipantes, 1, 20, "A quantidade de participantes deve ser entre 1 e 20 participantes.");
        
        if (!string.IsNullOrWhiteSpace(Observacao))
            AssertionConcern.AssertArgumentLength(Observacao, 0, 500, "A observação da festa não deve conter mais que 500 caracteres.");
    }
}