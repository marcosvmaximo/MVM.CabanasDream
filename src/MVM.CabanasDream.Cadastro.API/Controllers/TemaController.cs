using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVM.CabanasDream.Cadastro.API.Interfaces;
using MVM.CabanasDream.Cadastro.API.Models;
using MVM.CabanasDream.Cadastro.API.ViewModels;

namespace MVM.CabanasDream.Cadastro.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/tema")]
[Authorize(Roles = "Cliente")]
public class TemaController : MainController
{
    private readonly ITemaRepository _repository;
    private readonly ITemaService _service;

    public TemaController(ITemaRepository repository, ITemaService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<TemaDto>>> ObterCatalogo()
    {
        IEnumerable<Tema?> temas = await _repository.ObterTodosTemas();

        if (temas is null || !temas.Any())
            return CustomResponse(HttpStatusCode.NotFound, true, "Tema não encontrado", null);

        List<TemaDto> result = new List<TemaDto>();

        foreach (var tema in temas)
        {
            result.Add(MapearTema(tema));
        }

        return CustomResponse(HttpStatusCode.OK, true, "Requisição enviada com sucesso", result);
    }

    [HttpGet("{id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<TemaDto>> ObterTema(Guid id)
    {
        Tema? tema = await _repository.ObterTemaPorId(id);

        if (tema == null)
            return CustomResponse(HttpStatusCode.NotFound, true, "Tema não encontrado", null);

        return CustomResponse(HttpStatusCode.OK, true, "Requisição enviada com sucesso", MapearTema(tema));
    }

    /// <summary>
    /// Obtem todas empresas cadastradas.
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [Authorize(Roles = "Administrador")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<TemaDto>> CadastrarTema(TemaViewModel tema)
    {
        var imagemNome = $"{Guid.NewGuid()}_{tema.Imagem}";
        tema.Imagem = imagemNome;

        UploadArquivo(tema.ImagemUpload, tema.Imagem);
      
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);            
            return CustomResponse(HttpStatusCode.NotFound, false, "Tema inserido inválido", errors);
        }
  
        var result = await _service.CadastrarTema(tema);

        return CustomResponse(HttpStatusCode.Created, true, "Tema criado com sucesso.", result);
    }

    private TemaDto MapearTema(Tema tema)
    {
        List<ProdutoDto> produtos = new List<ProdutoDto>();

        foreach (var produto in tema.Produtos)
        {
            produtos.Add(new ProdutoDto(produto.Id, produto.Nome, produto.Tipo));
        }

        return new TemaDto(tema.Id, tema.Nome, tema.Descricao, tema.PrecoBase, tema.Imagem, tema.ImagemUpload,
            tema.Disponibilidade, produtos);
    }
}