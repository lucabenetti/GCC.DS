using GCC.Business.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCC.Data.Mapeamento
{
    public class ConsultaMapeamento : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Data).IsRequired();
            builder.Property(c => c.Duracao).IsRequired();
            builder.Property(c => c.Medico).IsRequired();
            builder.Property(c => c.Paciente).IsRequired();
            builder.Property(c => c.Prontuario).IsRequired();
            builder.Property(c => c.Realizada).IsRequired();
            builder.ToTable("Consulta");
        }
    }
}
