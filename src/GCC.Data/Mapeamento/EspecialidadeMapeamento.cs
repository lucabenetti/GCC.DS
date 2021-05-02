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
    public class EspecialidadeMapeamento : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.ToTable("Especialidade");
        }
    }
}
