using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class AdministradorMap : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.ToTable("Administradores");

        builder.HasKey(c => c.Id);
        
        builder.Property(a => a.TimeStamp)
            .HasColumnName("TimeStamp")
            .HasColumnType("datetime(6)")
            .IsRequired();
        
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.Property(a => a.Nome)
            .HasColumnName("nome_completo")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(a => a.DataNascimento)
            .HasColumnName("data_nascimento")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(a => a.Cpf)
            .HasColumnName("cpf")
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(a => a.Rg)
            .HasColumnName("rg")
            .HasColumnType("varchar(14)")
            .IsRequired();

        builder.Property(a => a.NivelPermissao)
            .HasColumnName("nivel_permissao")
            .HasColumnType("int")
            .IsRequired();
        //
        // // Mapeamento da relação um-para-muitos com Festa
        // builder.HasMany(a => a.Festas)
        //     .WithOne(x => x.Administrador)
        //     .HasForeignKey(x => x.AdministradorId)
        //     .HasPrincipalKey(a => a.Id);
    }
}