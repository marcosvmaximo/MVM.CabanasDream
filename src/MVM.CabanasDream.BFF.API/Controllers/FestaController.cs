using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVM.CabanasDream.BFF.API.Controllers;

[ApiController]
[Route("api/v1/festa")]
public class FestaController : ControllerBase
{
    [HttpGet("cliente/{id:guid}")]
    public async Task<ActionResult<IEnumerable<FestaResponse>>> ObterFestasPorCliente(Guid idCliente)
    {
        
    }

    [HttpPost()]
    public async Task<ActionResult<FestaResponse>> CriarFesta(FestaRequest request)
    {
        
    }

    [HttpPut("confirmar")]
    public async Task<ActionResult<FestaResponse>> ConfirmarFesta(ConfirmarFestaRequest request)
    {
        
    }
    
    [HttpPut("cancelar")]
    public async Task<ActionResult<FestaResponse>> CancelarFesta(CancelarFestaRequest request)
    {
        
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FestaResponse>> ObterPorId(Guid id)
    {
        
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FestaResponse>>> ObterTodas()
    {
        
    }
}