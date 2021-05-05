using GCC.Business.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCC.Data.Mapeamento
{
    public class DiaDeTrabalhoMapeamento : IEntityTypeConfiguration<JornadaDeTrabalho>
    {
        public void Configure(EntityTypeBuilder<JornadaDeTrabalho> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(d => d.HoraInicio).IsRequired();
            builder.Property(d => d.HoraInicioIntervalo).IsRequired();
            builder.Property(d => d.HoraFimIntervalo).IsRequired();
            builder.Property(d => d.HoraFim).IsRequired();
            builder.ToTable("DiaDeTrabalho");
        }
    }
}
