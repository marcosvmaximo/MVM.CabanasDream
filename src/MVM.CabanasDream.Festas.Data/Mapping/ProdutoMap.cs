using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain.TemaContext.Entities;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);
            
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("varchar(36)")
            .IsRequired();
        
        builder.Property(p => p.TimeStamp)
            .HasColumnName("TimeStamp")
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(p => p.Categoria)
            .HasColumnName("categoria")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(p => p.NumeroDeSerie)
            .HasColumnName("numero_de_serie")
            .HasColumnType("varchar(5)")
            .IsRequired();

        builder.OwnsOne(x => x.Valor, v =>
        {
            v.Property(p => p.ValorCompra)
                .HasColumnName("valor_compra")
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            v.Property(p => p.ValorLocacao)
                .HasColumnName("valor_locacao")
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
        });
        
        builder.Property(p => p.Alocado)
            .HasColumnName("alocado")
            .HasColumnType("tinyint")
            .IsRequired();

        // Mapeamento da relação muitos-para-um com Tema
        builder.HasOne(p => p.Tema)
            .WithMany(t => t.Produtos)
            .HasForeignKey(p => p.TemaId)
            .HasPrincipalKey(t => t.Id);

    }
}