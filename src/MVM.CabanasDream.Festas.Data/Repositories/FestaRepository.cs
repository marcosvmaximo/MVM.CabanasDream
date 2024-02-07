using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Festas.Data.Context;
using MVM.CabanasDream.Festas.Domain.FestaContext;
using MVM.CabanasDream.Festas.Domain.FestaContext.Entities;
using MVM.CabanasDream.Festas.Domain.FestaContext.Interfaces;
using MVM.CabanasDream.Festas.Domain.TemaContext;

namespace MVM.CabanasDream.Festas.Data.Repositories;

public class FestaRepository : IFestaRepository
{
    private readonly DataContext _context;
    private readonly ILogger<FestaRepository> _logger;
    public FestaRepository(DataContext context, ILogger<FestaRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IUnityOfWork UnityOfWork => _context;
    
    // public async Task<Cliente?> ObterClientePorId(Guid clienteId)
    // {
    //     try
    //     {
    //         return await _context.Clientes
    //             .Include(x => x.Festas)
    //             .FirstOrDefaultAsync(x => x.Id == clienteId);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, ex.Message);
    //         throw;
    //     }
    // }
    

    public async Task<Tema?> ObterTemaPorId(Guid temaId)
    {
        try
        {
            return await _context.Temas
                .Include(x => x.Festas)
                .FirstOrDefaultAsync(x => x.Id == temaId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Festa?>> ObterTodasFestas()
    {
        try
        {
            return await _context.Festas.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Festa?>> ObterFestasPorFiltro(Expression<Func<Festa, bool>> filtro)
    {
        try
        {
            IQueryable<Festa> query = _context.Festas;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query
                .Include(x => x.Administrador)
                .Include(x => x.Cliente)
                .Include(x => x.Tema)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Festa?>> ObterFestaPorCliente(Guid idCliente)
    {
        try
        {
            return _context.Festas
                .AsNoTracking()
                .Include(x => x.Administrador)
                .Include(x => x.Cliente)
                .Include(x => x.Tema)
                .Where(x => x.ClienteId == idCliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<Festa?> ObterFestaPorId(Guid festaId)
    {
        try
        {
            return await _context.Festas
                .AsNoTracking()
                .Include(x => x.Administrador)
                .Include(x => x.Cliente)
                .Include(x => x.Tema)
                .FirstOrDefaultAsync(x => x.Id == festaId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task SalvarFesta(Festa festa)
    {
        try
        {
            await _context.Festas.AddAsync(festa);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}