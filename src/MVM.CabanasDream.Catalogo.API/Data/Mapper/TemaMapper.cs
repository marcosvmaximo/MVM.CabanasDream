using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Catalogo.API.Models;

namespace MVM.CabanasDream.Catalogo.API.Data.Mapper;

public class TemaMapper : IEntityTypeConfiguration<Tema>
{
    public void Configure(EntityTypeBuilder<Tema> builder)
    {
        builder.ToTable("Temas");

        builder.HasKey(x => x.Id);
        //
        // builder.Property(x => x.Id)
        //     .HasColumnType("varchar(36)")
        //     .HasColumnName("id_tema");
        
        builder.Property(x => x.Nome)
            .HasColumnType("varchar(100)")
            .HasColumnName("nome");
        
        builder.Property(x => x.Descricao)
            .HasColumnType("varchar(500)")
            .HasColumnName("descricao");
        
        builder.Property(x => x.PrecoBase)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("preco_base");
        
        builder.Property(x => x.Imagem)
            .HasColumnType("varchar(500)")
            .HasColumnName("nome_imagem");
        
        builder.Property(x => x.ImagemUpload)
            .HasColumnType("longtext")
            .HasColumnName("imagem");
        
        builder.Property(x => x.Disponibilidade)
            .HasColumnType("bool")
            .HasColumnName("disponibilidade");

        builder.HasMany(x => x.Produtos)
            .WithOne(x => x.Tema)
            .HasForeignKey(x => x.TemaId);
    }
}