using GCC.Business.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.Data.Mapeamento
{
    public class PacienteMapeamento : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CPF)
                   .IsRequired()
                   .HasColumnType("varchar(11)");
            builder.Property(p => p.DataNascimento)
                   .IsRequired();
            builder.Property(p => p.Endereco)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.Property(p => p.Sexo)
                   .IsRequired();
            builder.Property(p => p.Telefone)
                   .HasColumnType("varchar(11)");
            builder.Property(p => p.NomeDaMae)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            builder.ToTable("Paciente");
        }
    }
}
