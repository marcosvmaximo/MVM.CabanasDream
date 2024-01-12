using System.Net;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.API.Controllers.Common;
using Newtonsoft.Json;

namespace MVM.CabanasDream.Festas.API.Configurations.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message); 
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        object response;
        context.Response.ContentType = "application/json";
        
        if (exception is DomainException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            response = new
            {
                httpCode = 400,
                success = false,
                message = "Ocorreu uma falha ao enviar a requisição.",
                data = exception.Message
            };
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            response = new
            {
                httpCode = 500,
                success = false,
                message = "Ocorreu um erro interno no servidor.",
                data = exception.Message
            };
        }
        
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}