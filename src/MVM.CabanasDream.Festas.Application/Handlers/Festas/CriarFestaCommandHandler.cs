using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.IntegrationsEvents;
using MVM.CabanasDream.Festas.Application.Commands.Festas;
using MVM.CabanasDream.Festas.Application.Validators.Festas;
using MVM.CabanasDream.Festas.Application.ViewModels.Festas;
using MVM.CabanasDream.Festas.Application.ViewModels.Temas;
using MVM.CabanasDream.Festas.Domain.FestaContext;
using MVM.CabanasDream.Festas.Domain.FestaContext.Entities;
using MVM.CabanasDream.Festas.Domain.FestaContext.Enum;
using MVM.CabanasDream.Festas.Domain.FestaContext.Interfaces;
using MVM.CabanasDream.Festas.Domain.FestaContext.ValueObjects;
using MVM.CabanasDream.Festas.Domain.Interfaces;
using MVM.CabanasDream.Festas.Domain.TemaContext;

namespace MVM.CabanasDream.Festas.Application.Handlers.Festas;

public class CriarFestaCommandHandler : Handler<CriarFestaCommand>
{
    private readonly IFestaRepository _festaRepository;
    private readonly ITemaRepository _temaRepository;

    public CriarFestaCommandHandler(IMessageBus bus, IFestaRepository festaRepository, ITemaRepository temaRepository) : base(bus)
    {
        _festaRepository = festaRepository;
        _temaRepository = temaRepository;
    }

    public override async Task<CommandResponse> Handle(CriarFestaCommand message, CancellationToken cancellationToken)
    {
        bool commandIsValid = ValidarComando<CriarFestaCommandValidator>(message);
        if (!commandIsValid) return ReturnResponse();

        var tema = await GetTema(message.TemaId);

        if (!ValidationResult.IsValid)
        {
            return ReturnResponse();
        }

        if (await PossuiFestaAtiva(message.ClienteId))
        {
            return ReturnResponse();
        }
        
        bool temaPossuiAgendamento = await VerificarDisponibilidadeTemaPorData(
            message.TemaId,
            message.DataRetirada,
            message.DataDevolucao);
        if (temaPossuiAgendamento) return ReturnResponse();

        var festa = MapFesta(message, tema);
        
        await _festaRepository.SalvarFesta(festa);
        await _festaRepository.UnityOfWork.Commit();
        
        return ReturnResponse(MapViewModel(festa));
    }

    private async Task<bool> PossuiFestaAtiva(Guid idCliente)
    {
        var enumerable = await _festaRepository.ObterFestaPorCliente(idCliente);
        var festasCliente = enumerable.ToList();
        
        if (!festasCliente.Any()) return false;
        
        var festasAtivas = festasCliente.Where(x =>
            x.Status == EStatusFesta.Pendente || x.Status == EStatusFesta.EmAndamento);

        if (festasAtivas.Any())
        {
            AddError("Cliente", "O Cliente já possuí uma festa pendente ou em andamento. Complete ou cancele a festa pendente e tente novamente");
            return true;
        }

        return false;
    }

    private Festa MapFesta(CriarFestaCommand message, Tema tema)
    {
        DataFesta dataFesta = new(message.DataRealizacao, message.DataRetirada, message.DataDevolucao);
        
        Festa festa = new(
            message.QuantidadeParticipantes,
            tema,
            dataFesta,
            message.ClienteId, 
            message.AdministradorId,
            message.Observacao);
        
        // Festa Criada Event
        // Fiscal pega o evento, obtem a festa, o cliente, o administrador e formula o contrato
        // Verifica exigencias
        // Cria contrato como pendente, aguardando pagamento
        // Festa será pendente e aguardando pagamento.
        festa.AddEvent(new FestaCriadaEvent(festa.Id, festa.TemaId, festa.ClienteId, festa.AdministradorId));
        
        return festa;
    }

    private FestaViewModel MapViewModel(Festa festa)
    {
        return new FestaViewModel(
            festa.Tema.Nome,
            festa.QuantidadeParticipantes,
            festa.Data.Realizacao,
            festa.Data.Retirada,
            festa.Data.Devolucao
            );
    }
    private async Task<Tema?> GetTema(Guid id)
    {
        var tema = await _temaRepository.ObterPorId(id);

        if (tema is null)
        {
            AddError("Tema","O Tema solicitado não foi encontrado. Verifique se o ID do Tema está correto e tente novamente.");
            return null;
        }

        if (!tema.Disponibilidade)
        {
            AddError("Tema","O Tema solicitado não está disponível no momento.");
            return null;
        }

        return tema;
    }

    private async Task<bool> VerificarDisponibilidadeTemaPorData(Guid idTema, DateTime dataInicio, DateTime dataFim)
    {
        // Adiciona um gap de 6 horas para data de retirada, e entrega
        // Ou seja, se alguem tiver outra festa do mesmo tema para retirar na data - 6h,
        // ela será exibida e logo indisponibilizada
        dataInicio = dataInicio.Subtract(TimeSpan.FromHours(6));
        dataFim = dataFim.AddHours(6);
        
        var festasAgendadas = await _festaRepository.ObterFestasPorFiltro(f => 
            f.TemaId == idTema && 
            f.Data.Retirada >= dataInicio && f.Data.Devolucao <= dataFim && 
            (f.Status == EStatusFesta.Pendente || f.Status == EStatusFesta.EmAndamento));

        if (festasAgendadas.Any())
        {
            AddError("Tema","O Tema solicitado não estará disponível na data selecionada, pois já está reservado para outra Festa.");
            return true;
        }

        return false;
    }
}