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
    public class MedicoMapeamento : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CPF)
                   .IsRequired()
                   .HasColumnType("varchar(11)");
            builder.Property(m => m.DataNascimento)
                   .IsRequired();
            builder.Property(m => m.Endereco)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.Property(m => m.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.Property(m => m.Sexo)
                   .IsRequired();
            builder.Property(m => m.Telefone)
                   .HasColumnType("varchar(11)");
            builder.ToTable("Medico");
        }
    }
}
