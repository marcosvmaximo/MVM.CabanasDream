using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Controllers;

[Route("api/v1/")]
public class FestaController : ControllerBase
{
    private readonly IFestaRepository _repository;
    private readonly IMessageBus _mediator;
    private readonly DomainNotificationHandler _notification;

    public FestaController(
        IFestaRepository repository,
        IMessageBus mediator,
        INotificationHandler<DomainNotification> notification)
    {
        _repository = repository;
        _mediator = mediator;
        _notification = (DomainNotificationHandler)notification;
    }
    
    [HttpGet("cliente/{idCliente:guid}")]
    public async Task<ActionResult<Festa?>> ObterFestasPorCliente([FromRoute] Guid idCliente)
    {
        var result = await _repository.ObterFestaPorCliente(idCliente);
        return Ok(result);
    }
    
    [HttpGet("")]
    public async Task<ActionResult<Festa?>> ObterFestas()
    {
        var result = await _repository.ObterTodasFestas();
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Festa?>> ObterFestaPorId([FromRoute] Guid id)
    {
        var result = await _repository.ObterFestaPorId(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<Festa?>> CriarFesta([FromBody] CriarFestaCommand request)
    {
        var response = await _mediator.SendCommand<CriarFestaCommand, CriarFestaViewModel>(request);
        
        if (await _notification.AnyNotification())
            return BadRequest(new
            {
                HttpCode = 400,
                Sucess = false,
                Message = "Ocorreu un problema ao enviar a requisição.",
                Data = await _notification.GetNotifications()
            });
        
        return Ok(new
        {
            HttpCode = 200,
            Sucess = true,
            Message = "Requisição enviada com sucesso.",
        });
    }

    [HttpPatch]
    public async Task<ActionResult<Festa?>> ConfirmarFesta()
    {
        
    }
    
    [HttpPatch]
    public async Task<ActionResult<Festa?>> RetirarFesta()
    {
        
    }
    
    [HttpPatch]
    public async Task<ActionResult<Festa?>> FinalizarFesta()
    {
        
    }
    
    [HttpPatch]
    public async Task<ActionResult<Festa?>> CancelarFesta()
    {
        
    }
}