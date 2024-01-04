using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Festas.API.Controllers.Common;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Application.ViewModels;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Controllers;

[Route("api/v1/")]
public class FestaController : ControllerCommon
{
    private readonly IFestaRepository _repository;
    private readonly IMediatorHandler _mediator;
    private readonly INotificationHandler _notification;

    public FestaController(IFestaRepository repository, IMediatorHandler mediator, INotificationHandler notification)
    {
        _repository = repository;
        _mediator = mediator;
        _notification = notification;
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
        var response = await _mediator.EnviarComando<CriarFestaCommand, CriarFestaViewModel>(request);

        if (response is null)
            return BadRequest(new
            {
                HttpCode = 400,
                Sucess = false,
                Message = "Ocorreu un problema ao enviar a requisição.",
                Data = _notification.GetNotifications()
            });
        
        return Ok(new
        {
            HttpCode = 200,
            Sucess = true,
            Message = "Requisição enviada com sucesso.",
        });
    }
}