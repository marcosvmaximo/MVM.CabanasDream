using System.Net;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.API.Controllers.Common;
using MVM.CabanasDream.Festas.API.Models;
using MVM.CabanasDream.Festas.Application.Commands.Temas;
using MVM.CabanasDream.Festas.Application.ViewModels.Temas;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Controllers;

[Route("api/{version:apiVersion}/tema")]
[ApiVersion("1.0")]
public class TemaController : ControllerCommon
{
    private readonly IFestaRepository _repository;

    public TemaController(
        IFestaRepository repository,
        IMessageBus messager,
        INotificationHandler<DomainNotification> notification) : base(messager, notification)
    {
        _repository = repository;
    }
    
    [HttpGet("produtos")]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<TemaViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TemaViewModel>>> ObterTodosTemasComProdutos()
    {
        var response = await _repository.ObterTodosTemas();
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (response == null) 
            return await CustomResponse(HttpStatusCode.NotFound);
        
        return await CustomResponse(HttpStatusCode.OK, response);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BaseResponse<TemaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TemaViewModel>> ObterTemaComProduto(Guid idTema)
    {
        var response = await _repository.ObterTemaPorId(idTema);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (response == null) 
            return await CustomResponse(HttpStatusCode.NotFound);
        
        return await CustomResponse(HttpStatusCode.OK, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<TemaViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TemaViewModel>>> ObterTemaPorFiltro([FromQuery] FiltroTema? filtro)
    {
        var response = await _repository.ObterTodosTemas(filtro);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (response == null) 
            return await CustomResponse(HttpStatusCode.NotFound);
        
        return await CustomResponse(HttpStatusCode.OK, response);
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(BaseResponse<>))]
    [ProducesResponseType(typeof(BaseResponse<TemaViewModel>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarTema(CriarTemaCommand request)
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

    [HttpPost("produto/{idTema:guid}")]
    public async Task<IActionResult> CadastrarProduto([FromBody]CriarProdutoCommand request, [FromRoute] Guid idTema)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        var response = await _messager.SendCommand(request);
        
        if (response.Success)
            return await CustomResponse(response, HttpStatusCode.Created);

        return await CustomResponse(response);
    }
}