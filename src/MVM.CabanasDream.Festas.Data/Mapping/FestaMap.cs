using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class FestaMap : IEntityTypeConfiguration<Festa>
    {
        public void Configure(EntityTypeBuilder<Festa> builder)
        {
            builder.ToTable("Festa_Contrato");

            builder.HasKey(f => f.Id);
            
            builder.Property(f => f.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(32)")
                .IsRequired();

            builder.Property(f => f.QuantidadeParticipantes)
                .HasColumnName("quantidade_participantes")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(f => f.DataRealizacao)
                .HasColumnName("data_realizacao")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(f => f.DataRetirada)
                .HasColumnName("data_retirada")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(f => f.DataDevolucao)
                .HasColumnName("data_devolucao")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(f => f.Observacao)
                .HasColumnName("observacao")
                .HasColumnType("varchar(500)");

            builder.Property(f => f.Status)
                .HasColumnName("status")
                .HasColumnType("int")
                .IsRequired();

            builder.OwnsOne(f => f.Contrato, c =>
            {
                c.Property(x => x.Assinado)
                    .HasColumnName("assinado")
                    .HasColumnType("bool")
                    .IsRequired();

                c.Property(x => x.Valor)
                    .HasColumnName("valor")
                    .HasColumnType("decimal(10, 2)")
                    .IsRequired();

                c.Property(x => x.Multa)
                    .HasColumnName("multa")
                    .HasColumnType("decimal(10, 2)")
                    .IsRequired();

            });
            
            // Mapeamento da relação muitos-para-um com Tema
            builder.HasOne(f => f.Tema)
                .WithMany()
                .HasForeignKey(f => f.TemaId)
                .HasConstraintName("fk_Festa_Temas_id") // Nome exclusivo para esta restrição
                .OnDelete(DeleteBehavior.Cascade);

            // Mapeamento da relação muitos-para-um com Cliente
            builder.HasOne(f => f.Cliente)
                .WithMany(c => c.Festas)
                .HasForeignKey(x => x.ClienteId)
                .HasConstraintName("fk_Festa_Clientes_id") // Nome exclusivo para esta restrição
                .OnDelete(DeleteBehavior.Cascade);

            // Mapeamento da relação muitos-para-um com Administrador
            builder.HasOne(f => f.Administrador)
                .WithMany(a => a.Festas)
                .HasForeignKey(x => x.AdministradorId)
                .HasConstraintName("fk_Festa_Administradores_id") // Nome exclusivo para esta restrição
                .OnDelete(DeleteBehavior.Cascade);

        }
    }