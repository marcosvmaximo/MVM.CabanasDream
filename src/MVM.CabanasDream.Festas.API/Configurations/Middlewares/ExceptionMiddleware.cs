using System.Net;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.API.Controllers.Common;
using Newtonsoft.Json;

namespace MVM.CabanasDream.Festas.API.Configurations.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Lógica de tratamento de exceções
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        BaseResponse<string> response;
        context.Response.ContentType = "application/json";
        
        if (exception is DomainException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            response = BaseResponse<string>.FailureResponse(exception.Message);
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            response = new BaseResponse<string>()
            {
                HttpCode = 500,
                Success = false,
                Message = "Ocorreu um erro interno no servidor.",
                Data = exception.Message
            };
        }
        
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}