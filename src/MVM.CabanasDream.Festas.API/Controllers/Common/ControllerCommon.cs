using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Festas.API.Controllers.Common;

[ApiController]
public abstract class ControllerCommon : ControllerBase
{
    protected readonly DomainNotificationHandler _notification;
    protected readonly IMessageBus _messager;

    public ControllerCommon(IMessageBus messager, INotificationHandler<DomainNotification> notification)
    {
        _messager = messager;
        _notification = (DomainNotificationHandler)notification;
    }
    
    /// <summary>
    /// Método resposável pela resposta envolvedo o Model State, transferindo os erros para o objeto padrão de resposta.
    /// </summary>
    /// <param name="response">ActionResult</param>
    /// <returns>Uma resposta Http inválida.</returns>
    protected async Task<ActionResult> CustomResponse(ModelStateDictionary modelState)
    {
        // Se modelo "Command ou DTO" for inválido, irá adicionar cada notificação à fila.
        if (!modelState.IsValid)
            await NotifyModelState(modelState);

        var response = CommandResponse.CustomResponse();
        return await CustomResponse(response);
    }
    
    /// <summary>
    /// Método centralizado das respostas possíveis.
    /// </summary>
    /// <param name="response">ActionResult</param>
    /// <returns>Uma resposta Http com objeto de retorno agregado.</returns>
    protected async Task<ActionResult> CustomResponse(CommandResponse response, HttpStatusCode? code = null)
    {
        var result = await NotifyTransfer(response);

        if (response.Data is HttpStatusCode) 
            code ??= (HttpStatusCode)response.Data;
        
        code ??= result.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        
        return code switch
        {
            HttpStatusCode.BadRequest => BadRequest(BaseResponse<Dictionary<string, object>>.FailureResponse(response.Errors)),
            
            HttpStatusCode.NotFound => NotFound(BaseResponse<Dictionary<string, object>>.FailureResponse(response.Errors, (int)HttpStatusCode.NotFound, "Dado informado não encontrado.", true)),

            HttpStatusCode.Created => Created("Post", BaseResponse<object>.SucessResponse(response.Data, (int)HttpStatusCode.Created, "Conteudo criado com sucesso.")),
            
            HttpStatusCode.NoContent => NoContent(),

            _ => Ok(BaseResponse<object>.SucessResponse(response.Data))
        };
    }
    protected async Task<ActionResult> CustomResponse(HttpStatusCode? code, object data = null)
    {
        return code switch
        {
            HttpStatusCode.BadRequest => BadRequest(BaseResponse<object>.FailureResponse(data)),
            
            HttpStatusCode.NotFound => NotFound(BaseResponse<object>.FailureResponse(data, (int)HttpStatusCode.NotFound, "Dado informado não encontrado.", true)),

            HttpStatusCode.Created => Created("Post", BaseResponse<object>.SucessResponse(data, (int)HttpStatusCode.Created, "Conteudo criado com sucesso.")),
            
            HttpStatusCode.NoContent => NoContent(),

            _ => Ok(BaseResponse<object>.SucessResponse(data))
        };
    }
    /// <summary>
    /// Transfere todas as notificações da fila, para o objeto de resposta padrão (Command Response)
    /// </summary>
    /// <returns>Um objeto de resposta com a união das notificações + erros</returns>
    private async Task<CommandResponse> NotifyTransfer(CommandResponse response)
    {
        var notifications = await _notification.GetNotifications();
        
        foreach (var n in notifications)
        {
            response.AddError(n.Property, n.Message);
        }

        return response;
    }
    
    /// <summary>
    /// Obtém os erros do Model State, e o adiciona a fila de notificações
    /// </summary>
    protected async Task NotifyModelState(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);

        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            await _messager.PublishNotification(new DomainNotification(erro.Exception.GetType().Name, erro.ErrorMessage));
        }
    }
}