using GCC.Business.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCC.Data.Mapeamento
{
    public class DiaDeTrabalhoMapeamento : IEntityTypeConfiguration<DiaDeTrabalho>
    {
        public void Configure(EntityTypeBuilder<DiaDeTrabalho> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(d => d.Dia).IsRequired();
            builder.Property(d => d.HoraInicio).IsRequired();
            builder.Property(d => d.HoraInicioIntervalo).IsRequired();
            builder.Property(d => d.HoraFimIntervalo).IsRequired();
            builder.Property(d => d.HoraFim).IsRequired();
            builder.ToTable("DiaDeTrabalho");
        }
    }
}
