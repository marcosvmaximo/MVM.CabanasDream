using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Cadastro.API.Models;

namespace MVM.CabanasDream.Cadastro.API.Data.Mapper;

public class ProdutoMapper : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(x => x.Id);

        // builder.Property(x => x.Id)
        //     .HasColumnType("varchar(36)")
        //     .HasColumnName("id_produto");
        
        builder.Property(x => x.Nome)
            .HasColumnType("varchar(100)")
            .HasColumnName("nome");
        
        builder.Property(x => x.Tipo)
            .HasColumnType("int")
            .HasColumnName("tipo");
        
        builder.Property(x => x.TemaId)
            .HasColumnType("varchar(36)")
            .HasColumnName("id_tema");

        builder.HasOne(x => x.Tema)
            .WithMany(x => x.Produtos)
            .HasForeignKey(x => x.TemaId);
    }
}