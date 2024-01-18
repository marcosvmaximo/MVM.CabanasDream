using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVM.CabanasDream.Cadastro.API.Models;

namespace MVM.CabanasDream.Cadastro.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
        
    }
    
    public DbSet<Tema> Temas { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContext)) ?? throw new InvalidOperationException());
    }
}