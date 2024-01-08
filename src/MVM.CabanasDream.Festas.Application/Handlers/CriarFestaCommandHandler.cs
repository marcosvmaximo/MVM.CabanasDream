using System.Net;
using System.Xml.Linq;
using FluentValidation.Results;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.Validators;
using MVM.CabanasDream.Festas.Application.ViewModels;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Enum;
using MVM.CabanasDream.Festas.Domain.Interfaces;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class CriarFestaCommandHandler : Handler<CriarFestaCommand>
{
    private readonly IFestaRepository _repository;

    public CriarFestaCommandHandler(IMessageBus bus, IFestaRepository repository) : base(bus)
    {
        _repository = repository;
    }

    public override async Task<CommandResult> Handle(CriarFestaCommand message, CancellationToken cancellationToken)
    {
        bool commandIsValid = ValidarComando<CriarFestaCommandValidator>(message);
        if (!commandIsValid) return CustomResponse();

        var tema = await GetTema(message.TemaId);
        var administrador = await GetAdministrador(message.AdministradorId);
        var cliente = await GetCliente(message.ClienteId);
        
        if (!ValidationResult.IsValid) return CustomResponse();
        
        bool possuiFestasAgendadas = await TemFestasAgendadasParaTemaNoPeriodo(
            message.TemaId,
            message.DataRetirada,
            message.DataDevolucao);
        if (possuiFestasAgendadas) return CustomResponse();

        var festa = MapFesta(message, tema!, cliente!, administrador!);
        await _repository.SalvarFesta(festa);
        
        return CustomResponse(MapViewModel(festa));
    }

    private Festa MapFesta(CriarFestaCommand message, Tema tema, Cliente cliente, Administrador administrador)
    {
        Festa festa = new(
            message.QuantidadeParticipantes,
            message.DataRealizacao,
            message.DataRetirada,
            message.DataDevolucao,
            tema, cliente,
            administrador,
            message.Observacao);
        
        Contrato contrato = new(festa.ObterValorFesta(), festa.ObterValorMulta());
        festa.AssociarContrato(contrato);
        
        return festa;
    }

    private CriarFestaViewModel MapViewModel(Festa festa)
    {
        return new CriarFestaViewModel(
            festa.Tema.Nome,
            festa.Cliente.Nome,
            festa.QuantidadeParticipantes,
            festa.DataRealizacao,
            festa.DataRetirada,
            festa.DataDevolucao,
            festa.Contrato.Valor,
            festa.Contrato.Multa,
            festa.Tema.Produtos);
    }
    private async Task<Tema?> GetTema(Guid id)
    {
        var tema = await _repository.ObterTemaPorId(id);

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

    private async Task<bool> TemFestasAgendadasParaTemaNoPeriodo(Guid idTema, DateTime dataInicio, DateTime dataFim)
    {
        // Adiciona um gap de 6 horas para data de retirada, e entrega
        // Ou seja, se alguem tiver outra festa do mesmo tema para retirar na data - 6h,
        // ela será exibida e logo indisponibilizada
        dataInicio = dataInicio.Subtract(TimeSpan.FromHours(6));
        dataFim = dataFim.AddHours(6);
        
        var festasAgendadas = await _repository.ObterFestasPorFiltro(f => 
            f.TemaId == idTema && 
            f.DataRetirada >= dataInicio && f.DataDevolucao <= dataFim && 
            (f.Status == EStatusFesta.Pendente || f.Status == EStatusFesta.EmAndamento));

        if (festasAgendadas.Any())
        {
            AddError("Tema","O Tema solicitado não estará disponível na data selecionada, pois já está reservado para outra Festa.");
            return true;
        }

        return false;
    }
    
    private async Task<Cliente?> GetCliente(Guid id)
    {
        var cliente = await _repository.ObterClientePorId(id);
        
        if(cliente is null)
            AddError("Cliente","O Cliente solicitado não foi encontrado. Verifique se o ID do Cliente está correto e tente novamente.");

        return cliente;
    }
    
    private async Task<Administrador?> GetAdministrador(Guid id)
    {
        var administrador = await _repository.ObterAdministradorPorId(id);
        
        if(administrador is null)
            AddError("Administrador","O Administrador solicitado não foi encontrado. Verifique se o ID do Administrador está correto e tente novamente.");

        return administrador;
    }
}