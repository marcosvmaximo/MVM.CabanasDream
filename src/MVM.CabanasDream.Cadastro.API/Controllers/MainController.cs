using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MVM.CabanasDream.Cadastro.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    
    protected ActionResult CustomResponse(HttpStatusCode code, bool sucess, string message, object? result)
    {
        var response = new
        {
            HttpCode = (int)code,
            Sucess = sucess,
            Message = message,
            Result = result
        };

        return new ObjectResult(response);
    }
    
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        return CustomResponse(HttpStatusCode.InternalServerError, false, "Falha na aplicação",
            null);
    }
}