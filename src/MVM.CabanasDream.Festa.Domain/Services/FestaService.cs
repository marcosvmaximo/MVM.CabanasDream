using MVM.CabanasDream.Festa.Domain.Interfaces;

namespace MVM.CabanasDream.Festa.Domain.Services;

public class FestaService : IFestaService
{
    // public FestaService(IFestaRepository repository)
    // {
    //     _repository = repository;
    // }
    
    public Task<Festa> CriarLocacaoFesta(Guid temaId, Guid clienteId, Guid adminsitradorId, int quantidadeParticipantes,
        DateTime dataRetirada, DateTime dataEntrega, DateTime dataRealizacao, string? observacao = null)
    {
        throw new NotImplementedException();
    }

    public Task<Festa> CancelarFesta(Guid festaId, string motivo)
    {
        throw new NotImplementedException();
    }

    public Task<Festa> ConfirmarFesta(Guid festaId, object pagamento)
    {
        throw new NotImplementedException();
    }

    public Task<Festa> ConcluirFesta(Guid festaId)
    {
        throw new NotImplementedException();
    }
}