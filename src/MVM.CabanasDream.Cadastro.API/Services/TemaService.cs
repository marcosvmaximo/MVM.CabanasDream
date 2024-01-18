using MVM.CabanasDream.Cadastro.API.Interfaces;
using MVM.CabanasDream.Cadastro.API.Models;
using MVM.CabanasDream.Cadastro.API.ViewModels;

namespace MVM.CabanasDream.Cadastro.API.Services;

public class TemaService : ITemaService
{
    private readonly ITemaRepository _repository;

    public TemaService(ITemaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TemaDto> CadastrarTema(TemaViewModel model)
    {
        var tema = new Tema(model.Nome,
                            model.Descricao,
                            model.PrecoBase,
                            model.Disponibilidade,
                            model.Imagem,
                            model.ImagemUpload);

        List<ProdutoDto> produtos = new List<ProdutoDto>();
        
        foreach (ProdutoViewModel p in model.Produtos)
        {
            Produto? produto = await _repository.ObterProdutoPorId(p.Id);

            if (produto is not null)
            {
                tema.AdicionarProduto(produto);
                produtos.Add(new ProdutoDto(produto.Id, produto.Nome, produto.Tipo));
            }
        }

        await _repository.CadastrarTema(tema);
        
        return new TemaDto(tema.Id, tema.Nome, tema.Descricao, tema.PrecoBase, tema.Imagem, tema.ImagemUpload, tema.Disponibilidade, produtos);
    }
}