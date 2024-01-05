using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Data;
using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Core.Messages.Common;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Data.Context;

public class DataContext : DbContext, IUnityOfWork
{
    private readonly IMessageBus _bus;

    public DataContext(DbContextOptions<DataContext> opt, IMessageBus bus) : base(opt)
    {
        _bus = bus;
    }

    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }
    public DbSet<Festa> Festas { get; set; }
    public DbSet<Tema> Temas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Administrador> Administradores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<Entity>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public async Task<bool> Commit()
    {
        bool result = await SaveChangesAsync() > 0;
        
        if (result)
        {
            await _bus.PublishEvents(this);
        }
        else
        {
            await _bus.PublishNotification(
                new DomainNotification("Evento", "Falha ao salvar a entidade, eventos não foram enviados."));
        }

        return result;
    }
}

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var conectString = "Server=localhost; Port=3307; Database=cabanas-dream; Uid=root;Pwd=8837;";

        optionsBuilder.UseMySql(conectString, ServerVersion.AutoDetect(conectString));

        return new DataContext(optionsBuilder.Options);
    }
}