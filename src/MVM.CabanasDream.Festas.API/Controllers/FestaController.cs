using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.API.Controllers.Common;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Controllers;

[Route("api/v1/festa")]
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
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<FestaViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<FestaViewModel>>> ObterFestasPorCliente([FromRoute] Guid idCliente)
    {
        var response = await _repository.ObterFestaPorCliente(idCliente);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (response == null) 
            return await CustomResponse(HttpStatusCode.NotFound);
        
        return await CustomResponse(HttpStatusCode.OK, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FestaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<FestaViewModel?>>> ObterFestas()
    {
        var response = await _repository.ObterTodasFestas();
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (response == null) 
            return await CustomResponse(HttpStatusCode.NotFound);
        
        return await CustomResponse(HttpStatusCode.OK, response);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(FestaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FestaViewModel>> ObterFestaPorId([FromRoute] Guid id)
    {
        var response = await _repository.ObterFestaPorId(id);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (response == null) 
            return await CustomResponse(HttpStatusCode.NotFound);
        
        return await CustomResponse(HttpStatusCode.OK, response);
    }

    
    [HttpPost]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(BaseResponse<FestaViewModel>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FestaViewModel>> CriarFesta([FromBody] CriarFestaCommand request)
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
    
    [HttpPatch("confirmar")]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmarFesta(ConfirmarFestaCommand request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        var response = await _messager.SendCommand(request);
        
        if (response.Success)
        {
            return await CustomResponse(response, HttpStatusCode.NoContent);
        }
        
        return await CustomResponse(response);
    }
    
    [HttpPatch("retirar")]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RetirarFesta(RetirarFestaCommand request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        var response = await _messager.SendCommand(request);
        
        if (response.Success)
        {
            return await CustomResponse(response, HttpStatusCode.NoContent);
        }
        
        return await CustomResponse(response);
    }
    
    [HttpPatch("finalizar")]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FinalizarFesta(FinalizarFestaCommand request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        var response = await _messager.SendCommand(request);
        
        if (response.Success)
        {
            return await CustomResponse(response, HttpStatusCode.NoContent);
        }
        
        return await CustomResponse(response);
    }
    
    [HttpPatch("cancelar")]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelarFesta(CancelarFestaCommand request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        var response = await _messager.SendCommand(request);
        
        if (response.Success)
        {
            return await CustomResponse(response, HttpStatusCode.NoContent);
        }
        
        return await CustomResponse(response);
    }
}