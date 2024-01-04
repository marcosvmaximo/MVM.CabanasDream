using System.Net;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.Validators;
using MVM.CabanasDream.Festas.Application.ViewModels;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Interfaces;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festas.Application.Handlers;

public class CriarFestaCommandHandler : Handler<CriarFestaCommand, CriarFestaViewModel>
{
    private readonly IFestaRepository _repository;

    public CriarFestaCommandHandler(IMediatorHandler mediator, IFestaRepository repository) : base(mediator)
    {
        _repository = repository;
    }

    public override async Task<CriarFestaViewModel?> Handle(CriarFestaCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando<CriarFestaCommandValidator>(request))
            return null;

        Tema? tema = await _repository.ObterTemaPorId(request.TemaId);
        if (tema is null)
        {
            await _mediator.PublicarNotificacao(new DomainNotification("Tema informado não foi encontrado ou não existe"));
            return null;
        }
        // Verificar se Tema está disponiveis nas datas informadas
        
        Cliente? cliente = await _repository.ObterClientePorId(request.ClienteId);
        if (cliente is null)
        {
            await _mediator.PublicarNotificacao(new DomainNotification("Cliente informado não foi encontrado ou não existe"));
            return null;
        }
        
        // Verificar se o cliente não possuí pendencias
        
        Administrador? administrador = await _repository.ObterAdministradorPorId(request.AdministradorId);
        if (administrador is null)
        {
            await _mediator.PublicarNotificacao(new DomainNotification("Administrador informado não foi encontrado ou não existe"));
            return null;
        }

        Festa festa = new(request.QuantidadeParticipantes, request.DataRealizacao, request.DataRetirada,
            request.DataDevolucao, tema, cliente, administrador, request.Observacao);
        Contrato contrato = new(festa.ObterValorFesta(), festa.ObterValorMulta());
        festa.AssociarContrato(contrato);

        await _repository.SalvarFesta(festa);

        var viewModelResponse = new CriarFestaViewModel(
            tema.Nome, cliente.Nome, festa.QuantidadeParticipantes,
            festa.DataRealizacao, festa.DataRetirada, festa.DataDevolucao,
            contrato.Valor, contrato.Multa, tema.Produtos);
        return viewModelResponse;
    }
}