using GCC.Business.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCC.Data.Mapeamento
{
    public class SecretariaMapeamento : IEntityTypeConfiguration<Secretaria>
    {
        public void Configure(EntityTypeBuilder<Secretaria> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CPF)
                   .IsRequired()
                   .HasColumnType("varchar(11)");
            builder.Property(s => s.DataNascimento)
                   .IsRequired();
            builder.Property(s => s.Endereco)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.Property(s => s.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            builder.Property(s => s.Sexo)
                   .IsRequired();
            builder.Property(s => s.Telefone)
                   .HasColumnType("varchar(11)");
            builder.ToTable("Secretaria");
        }
    }
}
