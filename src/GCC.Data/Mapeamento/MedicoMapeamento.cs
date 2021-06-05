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

            builder.OwnsOne(m => m.CRM, tf =>
            {
                tf.Property(c => c.Numero)
                .IsRequired()
                .HasColumnName("CRMNumero");

                tf.Property(c => c.UF)
                .IsRequired()
                .HasColumnName("CRMUF");
            });

            builder.OwnsOne(m => m.JornadaDeTrabalho, tf =>
            {
                tf.Property(j => j.Domingo).HasColumnName("Domingo");
                tf.Property(j => j.Segunda).HasColumnName("Segunda");
                tf.Property(j => j.Terca).HasColumnName("Terca");
                tf.Property(j => j.Quarta).HasColumnName("Quarta");
                tf.Property(j => j.Quinta).HasColumnName("Quinta");
                tf.Property(j => j.Sexta).HasColumnName("Sexta");
                tf.Property(j => j.Sabado).HasColumnName("Sabado");
                tf.Property(j => j.HoraInicio).HasColumnName("HoraInicio");
                tf.Property(j => j.HoraFim).HasColumnName("HoraFim");
                tf.Property(j => j.HoraInicioIntervalo).HasColumnName("HoraInicioIntervalo");
                tf.Property(j => j.HoraFimIntervalo).HasColumnName("HoraFimIntervalo");
            });

            builder.ToTable("Medico");
        }
    }
}
