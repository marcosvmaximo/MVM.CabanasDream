using System.Net;
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
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            HttpCode = 500,
            Success = false,
            Message = "Ocorreu um erro interno no servidor.",
            ErrorDetails = exception.Message
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}