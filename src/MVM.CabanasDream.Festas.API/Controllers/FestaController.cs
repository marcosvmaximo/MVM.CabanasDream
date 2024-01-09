using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.API.Controllers.Common;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Controllers;

[Route("api/v1/")]
public class FestaController : ControllerCommon
{
    private readonly IFestaRepository _repository;

    public FestaController(
        IFestaRepository repository,
        IMessageBus messager,
        INotificationHandler<DomainNotification> notification) : base(messager, notification)
    {
        _repository = repository;
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
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        var response = await _messager.SendCommand(request);
        
        if (response.Success)
        {
            return await CustomResponse(response, HttpStatusCode.Created);
        }
        return await CustomResponse(response);
    }
    //
    // [HttpPatch]
    // public async Task<ActionResult<Festa?>> ConfirmarFesta()
    // {
    //     
    // }
    //
    // [HttpPatch]
    // public async Task<ActionResult<Festa?>> RetirarFesta()
    // {
    //     
    // }
    //
    // [HttpPatch]
    // public async Task<ActionResult<Festa?>> FinalizarFesta()
    // {
    //     
    // }
    //
    // [HttpPatch]
    // public async Task<ActionResult<Festa?>> CancelarFesta()
    // {
    //     
    // }
}