using MVM.CabanasDream.Festa.Domain.Entities;

namespace MVM.CabanasDream.Festa.Domain.Interfaces;

public interface IFestaService
{
    Task<Festa> CriarLocacaoFesta(Guid temaId, Guid clienteId, Guid adminsitradorId, int quantidadeParticipantes,
        DateTime dataRetirada, DateTime dataEntrega, DateTime dataRealizacao, string? observacao = null);
    Task<Festa> CancelarFesta(Guid festaId, string motivo);
    Task<Festa> ConfirmarFesta(Guid festaId, object pagamento);
    Task<Festa> ConcluirFesta(Guid festaId);
}