using Microsoft.AspNetCore.Mvc;

namespace MVM.CabanasDream.Festas.API.Controllers;

public class TemaController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemaViewModel>>> ObterTodosTemasComProdutos()
    {
        
    }
    
    [HttpGet]
    public async Task<ActionResult<TemaViewModel>> ObterTemaComProduto()
    {
        
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemaViewModel>>> ObterTemaPorFiltro()
    {
        
    }
    
}