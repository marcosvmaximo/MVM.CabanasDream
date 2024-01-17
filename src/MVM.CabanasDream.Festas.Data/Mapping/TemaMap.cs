using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class TemaMap : IEntityTypeConfiguration<Tema>
{
    public void Configure(EntityTypeBuilder<Tema> builder)
    {
        builder.ToTable("Temas");

        builder.HasKey(c => c.Id);
        
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .HasColumnType("varchar(36)")
            .IsRequired();
        
        builder.Property(t => t.TimeStamp)
            .HasColumnName("TimeStamp")
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder.Property(t => t.Nome)
            .HasColumnName("nome")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(t => t.Disponibilidade)
            .HasColumnName("disponibilidade")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder.Property(t => t.PrecoBase)
            .HasColumnName("preco_base")
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(t => t.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("varchar(500)");
    }
}