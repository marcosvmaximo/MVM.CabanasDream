using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Festas.Data.Context;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Interfaces;

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
    public async Task<Cliente?> ObterClientePorId(Guid clienteId)
    {
        try
        {
            return await _context.Clientes
                .Include(x => x.Festas)
                .FirstOrDefaultAsync(x => x.Id == clienteId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<Administrador?> ObterAdministradorPorId(Guid administradorId)
    {
        try
        {
            return await _context.Administradores
                .Include(x => x.Festas)
                .FirstOrDefaultAsync(x => x.Id == administradorId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

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

    public async Task<Festa?> ObterFestaPorId(Guid festaId)
    {
        try
        {
            return await _context.Festas
                .FirstOrDefaultAsync(x => x.Id == festaId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task SalvarFesta(Festas.Domain.Festa festa)
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