using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Data.Mapping;

public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);
            
            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(32)")
                .IsRequired();
            
            builder.Property(a => a.TimeStamp)
                .HasColumnName("TimeStamp")
                .HasColumnType("datetime(6)")
                .IsRequired();
            
            builder.Property(c => c.Nome)
                .HasColumnName("nome_completo")
                .HasColumnType("varchar(100)")
                .IsRequired();
            
            builder.Property(c => c.DataNascimento)
                .HasColumnName("data_nascimento")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.Cpf)
                .HasColumnName("cpf")
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.Property(c => c.Rg)
                .HasColumnName("rg")
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.OwnsOne(c => c.Endereco, x =>
            {
                x.Property(e => e.Logradouro)
                    .HasColumnName("logradouro")
                    .HasColumnType("varchar(100)");

                x.Property(e => e.Bairro)
                    .HasColumnName("bairro")
                    .HasColumnType("varchar(100)");

                x.Property(e => e.Cidade)
                    .HasColumnName("cidade")
                    .HasColumnType("varchar(100)");

                x.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("varchar(2)");

                x.Property(e => e.Pais)
                    .HasColumnName("pais")
                    .HasColumnType("varchar(100)");

                x.Property(e => e.Cep)
                    .HasColumnName("cep")
                    .HasColumnType("varchar(8)");
            });
            
            builder.OwnsOne(c => c.Contato, x =>
            {
                x.Property(c => c.Ddd)
                    .HasColumnName("ddd")
                    .HasColumnType("varchar(2)");

                x.Property(c => c.Telefone)
                    .HasColumnName("telefone")
                    .HasColumnType("varchar(10)");

                x.Property(c => c.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");
            });
        }
    }