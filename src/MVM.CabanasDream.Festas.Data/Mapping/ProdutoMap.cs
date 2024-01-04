using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);
            
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.NumeroSerie)
            .HasColumnName("numero_de_serie")
            .HasColumnType("varchar(32)");

        builder.Property(p => p.ValorCompra)
            .HasColumnName("valor_compra")
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(p => p.ValorLocacao)
            .HasColumnName("valor_locacao")
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        // Mapeamento da relação muitos-para-um com Tema
        builder.HasOne(p => p.Tema)
            .WithMany(t => t.Produtos)
            .HasForeignKey(p => p.TemaId)
            .HasConstraintName("fk_Produto_Temas_id")
            .OnDelete(DeleteBehavior.Restrict);

    }
}