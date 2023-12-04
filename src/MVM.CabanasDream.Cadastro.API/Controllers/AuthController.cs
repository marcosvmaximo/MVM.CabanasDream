using System.Text;
using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Cadastro.API.Models;

namespace MVM.CabanasDream.Cadastro.API.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
        // Insere credenciais
        // Verifica a autenticidade no banco
        // Se sim então usa um método e gera o token
        // O token é dado ao usuário

        // O usuário poderá enviar os tokens em sua requisição porém como o token será lido pelos outros
    }


    [HttpPost]
    public async Task<ActionResult> CadastrarCliente(ClienteViewModel model)
    {
        
    }

    public void GerarToken()
    {
        var secretKey = _configuration.GetValue<string>("Secret");
        var key = Encoding.ASCII.GetBytes(secretKey);
    }
}