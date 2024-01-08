using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.ViewModels;

namespace MVM.CabanasDream.Festas.Application.Commands;

public class CriarFestaCommand : Command
{
    public Guid TemaId { get; set; }   
    public Guid ClienteId { get; set; }
    public Guid AdministradorId { get; set; }   
    public DateTime DataRetirada { get; set; }
    public DateTime DataDevolucao { get; set; }
    public DateTime DataRealizacao { get; set; }
    public int QuantidadeParticipantes { get; set; }
    public string? Observacao { get; set; }
}