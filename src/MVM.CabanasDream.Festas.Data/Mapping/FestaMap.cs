using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain;
using MVM.CabanasDream.Festas.Domain.FestaContext;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class FestaMap : IEntityTypeConfiguration<Festa>
    {
        public void Configure(EntityTypeBuilder<Festa> builder)
        {
            builder.ToTable("Festas");

            builder.HasKey(f => f.Id);
            
            builder.Property(f => f.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(36)")
                .IsRequired();
            
            builder.Property(f => f.TimeStamp)
                .HasColumnName("TimeStamp")
                .HasColumnType("datetime(6)")
                .IsRequired();

            builder.Property(f => f.QuantidadeParticipantes)
                .HasColumnName("quantidade_participantes")
                .HasColumnType("int")
                .IsRequired();

            builder.OwnsOne(x => x.Data, d =>
            {
                d.Property(f => f.Realizacao)
                    .HasColumnName("data_realizacao")
                    .HasColumnType("date")
                    .IsRequired();

                d.Property(f => f.Retirada)
                    .HasColumnName("data_retirada")
                    .HasColumnType("date")
                    .IsRequired();

                d.Property(f => f.Devolucao)
                    .HasColumnName("data_devolucao")
                    .HasColumnType("date")
                    .IsRequired();
            });

            builder.Property(f => f.Observacao)
                .HasColumnName("observacao")
                .HasColumnType("varchar(500)");

            builder.Property(f => f.Status)
                .HasColumnName("festa_status")
                .HasColumnType("int")
                .IsRequired();
            
            builder.Property(f => f.ClienteId)
                .HasColumnName("ClienteId")
                .HasColumnType("varchar(36)")
                .IsRequired();
            
            builder.Property(f => f.AdministradorId)
                .HasColumnName("AdministradorId")
                .HasColumnType("varchar(36)")
                .IsRequired();
            
            // Mapeamento da relação muitos-para-um com Tema
            builder.HasOne(f => f.Tema)
                .WithMany(t => t.Festas)
                .HasForeignKey(f => f.TemaId)
                .HasPrincipalKey(t => t.Id);
            
        }
    }