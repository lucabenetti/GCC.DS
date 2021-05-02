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
    public class ProntuarioMapeamento : IEntityTypeConfiguration<Prontuario>
    {
        public void Configure(EntityTypeBuilder<Prontuario> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Descricao)
                   .IsRequired()
                   .HasColumnType("varchar(500)"); ;
            builder.ToTable("Prontuario");
        }
    }
}
