using System.Data;
using Microsoft.EntityFrameworkCore;
using MVM.CabanasDream.Cadastro.API.Interfaces;
using MVM.CabanasDream.Cadastro.API.Models;

namespace MVM.CabanasDream.Cadastro.API.Data;

public class TemaRepository : ITemaRepository
{
    private readonly DataContext _context;
    private readonly ILogger<TemaRepository> _logger;

    public TemaRepository(DataContext context, ILogger<TemaRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task CadastrarTema(Tema tema)
    {
        try
        {
            await _context.Temas.AddAsync(tema);
            _logger.LogInformation("Tema cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu uma falha ao salvar o Tema", ex);            
            throw new DataException("Erro ao realizar a consulta no banco de dados");
        }

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tema?>> ObterTodosTemas()
    {
        try
        {
            var temas = await _context.Temas.AsNoTracking()
                .Include(x => x.Produtos)
                .ToListAsync();
            
            _logger.LogInformation("Temas obtidos com sucesso.");
            return temas;
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu uma falha ao obter todos os Temas", ex);
            throw new DataException("Erro ao realizar a consulta no banco de dados");
        }
    }

    public async Task<Tema?> ObterTemaPorId(Guid id)
    {
        try
        {
            var tema = await _context.Temas.AsNoTracking()
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            _logger.LogInformation("Tema obtido com sucesso.");
            return tema;
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu uma falha ao obte o Tema", ex);
            throw new DataException("Erro ao realizar a consulta no banco de dados");
        }
    }

    public async Task<Produto?> ObterProdutoPorId(Guid id)
    {        
        try
        {
            var produto = await _context.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            _logger.LogInformation("Produto obtido com sucesso.");
            return produto;
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu uma falha ao obte o Produto", ex);
            throw new DataException("Erro ao realizar a consulta no banco de dados");
        }
    }
}