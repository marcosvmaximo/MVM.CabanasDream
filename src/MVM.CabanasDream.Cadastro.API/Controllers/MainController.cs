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

    protected bool UploadArquivo(string arquivo, string imgNome)
    {
        var imageDataByteArray = Convert.FromBase64String(arquivo);

        if (String.IsNullOrEmpty(arquivo) || arquivo.Length <= 0)
        {
            ModelState.AddModelError(string.Empty, "Forneça a imagem desse Tema");
            return false;
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", $"{imgNome}.jpg");

        if (System.IO.File.Exists(filePath))
        {
            ModelState.AddModelError(string.Empty, "Já existe uma imagem com esse nome");
            return false;
        }
        
        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }
    
    
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        return CustomResponse(HttpStatusCode.InternalServerError, false, "Falha na aplicação",
            null);
    }
}